using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MEF
{
    public class MEFBootstrapper
    {
        AggregateCatalog catalog;
        protected ICollection<ComposablePartCatalog> Catalogs;
        public MEFBootstrapper()
        {
            catalog = new AggregateCatalog();
            Catalogs = catalog.Catalogs;
        }

        public CompositionContainer InitializeContainer()
        {
            ConfigureCatalog();
            CompositionContainer container = new CompositionContainer(catalog);
            return container;
        }
        public CompositionContainer InitializeContainer(ICollection<ComposablePartCatalog> catalogParts)
        {
            ConfigureCatalog();
            if (catalogParts != null)
                foreach (var part in catalogParts)
                    Catalogs.Add(part);

            CompositionContainer container = new CompositionContainer(catalog);
            return container;
        }
        public static CompositionContainer InitContainer(ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalog = new AggregateCatalog();

            if (catalogParts != null)
                foreach (var part in catalogParts)
                    catalog.Catalogs.Add(part);

            CompositionContainer container = new CompositionContainer(catalog);
            return container;
        }
        protected virtual void ConfigureCatalog()
        {            
        }
    }
}
