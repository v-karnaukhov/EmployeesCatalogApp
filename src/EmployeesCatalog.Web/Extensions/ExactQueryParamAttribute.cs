using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace EmployeesCatalog.Web.Extensions
{
    /// <summary>
    /// Расширение для обхода AmbiguousActionException в контроллерах.
    /// <remarks>
    /// Позволяет развести действия вида
    /// employees/
    /// employees/?filter=...
    /// в разные методы контроллера без необходимости настройки роутинга.
    /// </remarks>
    /// </summary>
    public class ExactQueryParamAttribute : Attribute, IActionConstraint
    {
        private readonly string[] keys;

        public ExactQueryParamAttribute(params string[] keys)
        {
            this.keys = keys;
        }

        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            var query = context.RouteContext.HttpContext.Request.Query;
            return query.Count == keys.Length && keys.All(key => query.ContainsKey(key));
        }
    }
}
