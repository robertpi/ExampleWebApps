using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client.Indexes;
using Raven.Abstractions.Indexing;
using Raven.Client.Document;
using Raven.Abstractions.Data;
using HappyShopper.Model;

namespace FSharpMVC2.Web.Indexes
{
    /// <summary>
    /// Defines the index on Products in RavenDB
    /// </summary>
    public class Products_AllFields : AbstractIndexCreationTask
    {
        public override IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinitionBuilder<Product>
            {
                Map = products => from product in products
                                  select new 
                                  {
                                      product.IsActive,
                                      product.Name,
                                      product.Price,
                                      product.ProductCategories,
                                  }
            }.ToIndexDefinition(this.Conventions);
        }
    }
}