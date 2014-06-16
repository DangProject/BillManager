using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Threading;
using UI.Localization;

namespace UI.Controls
{
    public class TextPlaceables : TextBlock, IWeakEventListener
    {
        BindingExpression be;
        public TextPlaceables()
        {
            LanguageChangedEventManager.AddListener(
                      TranslationManager.Instance, this);
        }

        // TODO: These are wrong, because we need to handle numbered placeables <BeginPlaceable0>
        public string BeginPlaceable
        {
            get { return "<Hyperlink>"; }
        }
        public string EndPlaceable
        {
            get { return "</Hyperlink>"; }
        }

        // TODO: Refactor ParsePlaceables because it is long and evil.
        // TODO: Figure out how to handle re-entry problems, or refresh problems, when ParsePlaceables() might be called multiple times?
        // TODO: Maybe figure out how we can have the Text property changed later, and still work properly?
        //
        // TODO: Change the hard-coded test parsing into a 'foreach' statement that uses IEnumerable to pull out an object that contains:
        //      The text segement prior to the placeable text.
        //      The placeable text that was parsed
        //      The current Inline object that matches the index of the parsed placeable text
        // The IEnumerable algorithm should:
        //      Handle numbered placeables: <BeginPlaceable0>item 1<EndPlaceable0> more text <BeginPlaceable1>item 2<EndPlaceable1>
        //      Should utilize a regular expression for the parsing.
        //
        // Parse our Text property, creating a <Run> for each text segment, including the necessary placeables.

        TranslationData data;
        private void ParsePlaceables()
        {
            string text = data == null ? string.Empty : (string)data.Value;
            
            if (string.IsNullOrEmpty(text))
                return;

            int begin = 0;
            int end = 0;
            int parsedplaceablecount = 0;
            string remainder = text;

            // TODO: Maybe Consider using TextPointer's via Inlines.FirstInline.ContentStart instead of Inlines?
            //TextPointer tp = new TextPointer();

            // Create our own placeables collection in case we run into trouble parsing the string, and have to bail out.
            List<Inline> placeableinlines = new List<Inline>();

            // We can't index into 'Inlines' so make a list that we can index into.
            List<object> children = new List<object>();
            foreach (Inline il in Inlines)
                children.Add(il);

            while (true)
            {                
                begin = remainder.IndexOf(BeginPlaceable);
                end = remainder.IndexOf(EndPlaceable);

                if (begin == -1 && end == -1)
                    // done for now.
                    break;

                if (begin == -1 && end != -1 || begin != -1 && end == -1)
                {
                    Debug.Assert(false, string.Format("TextPlaceables: Mismatched count of {0} and {1}", BeginPlaceable, EndPlaceable));
                    return;
                }
                if (begin > end)
                {
                    Debug.Assert(false, string.Format("TextPlaceables: Found {0} before {1}", EndPlaceable, BeginPlaceable));
                    return;
                }

                // Don't add the text Run before the placeable if its empty.
                string prefixtext = remainder.Substring(0, begin);
                if (!string.IsNullOrEmpty(prefixtext))
                {
                    placeableinlines.Add(new Run
                    {
                        Text = prefixtext
                    });
                }

                // Remove the <BeginPlaceable> and <EndPlaceable> tags before adding...
                // Always add the placeable, even if its empty, since it may be an empty object that draws itself.
                string placeabletext = remainder.Substring(begin + BeginPlaceable.Length, end - (begin + BeginPlaceable.Length));
                                
                if (placeabletext == "®")
                {                    
                    // TODO: hard-coded solution for the SM example - we need to clone the original object's properties
                    //Run other = ((Run)children[parsedplaceablecount]);                    
                    placeableinlines.Add(new Run
                    {
                        Text = placeabletext,
                        BaselineAlignment = BaselineAlignment.Top,
                        FontSize = Convert.ToDouble(Math.Round(Convert.ToDecimal(FontSize) * (decimal).75))
                    });
                }
                else
                {   
                    for (int i = parsedplaceablecount; i < children.Count; i++)
                    {
                        parsedplaceablecount++;

                        if (children[i].GetType().IsAssignableFrom(typeof(Hyperlink)))
                        {
                            ((Hyperlink)children[i]).Inlines.Clear();
                            ((Hyperlink)children[i]).Inlines.Add(new Run(placeabletext));
                            placeableinlines.Add((Hyperlink)children[i]);
                            break;
                        }
                    }
                }

                // Bolded Run for early testing - can remove eventually
                //placeableinlines.Add(new Run
                //{
                //    Text = placeabletext,
                //    FontWeight = FontWeights.Bold
                //});

                remainder = remainder.Substring(end + EndPlaceable.Length);
            }

            if (!string.IsNullOrEmpty(remainder))
            {
                placeableinlines.Add(new Run
                {
                    Text = remainder
                });
            }

            // TODO: decide what to do about these warning conditions:
            //if(parsedplaceablecount > children.Count)
            //{
            //    Debug.Assert(false, "TextPlaceables: Parsed more placeables in the string than there are UI objects to place.");
            //    return;
            //}
            //else if (parsedplaceablecount < children.Count)
            //{
            //    Debug.Assert(false, "TextPlaceables: More UI objects than are placeables in the string.");
            //    return;
            //}

            // Success!!
            // Clear all the previous Inlines, since we've created new ones!
            Inlines.Clear();
            Inlines.AddRange(placeableinlines);
            parsedplaceablecount++;
        }
               
        public override void EndInit()
        {
            be = this.GetBindingExpression(TextProperty);

            if (be != null)
            {
                data = be.ParentBinding.Source as TranslationData;
                data.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(data_PropertyChanged);
                base.EndInit();                
            }

            ParsePlaceables();
        }

        void data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Thread.CurrentThread == Application.Current.Dispatcher.Thread)
            {
                //re-parse hyperlinks when text changes
                ParsePlaceables();
            }
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (Thread.CurrentThread == Application.Current.Dispatcher.Thread)
            {
                if (managerType == typeof(LanguageChangedEventManager))
                {
                    ParsePlaceables();
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
