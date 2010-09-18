using System.Web;
using System.Web.Mvc;
using Northwind.Core;
using System.Collections.Generic;

namespace Northwind.Web.Views.Categories
{
    /// <summary>
    /// This is an example of using a code-behind page with an ASP.NET MVC view.
    /// It's generally considered better practice to avoid code-behind pages within MVC.
    /// </summary>
    public partial class Index : ViewPage<IList<Category>>
    {
        public void Page_Load() {
            categoryList.DataSource = ViewData.Model;
            categoryList.DataBind();
        }
    }
}
