using Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    class ArticleService
    {
        private readonly ArticlesContext context;
        public ArticleService(ArticlesContext context)
        {
            this.context = context;
        }
        public ArticlesContext GetContext()
        {
            return context;
        }
    }
}
