using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestWindSystem.BLL
{
    public class BusinessRuleException : Exception // I inherit to provide a "specialized" kind of exception
    {
        // Fields that are readonly can only have their values assigned immediately or in the constructor
        public readonly ICollection<Exception> Errors = new List<Exception>();
        public readonly string ExecutionContext;

        public BusinessRuleException(string context)
        {
            // ?? is the Null Coallation Operator
            ExecutionContext = context ?? "No context supplied";
        }

        public BusinessRuleException(string context, List<Exception> violations) : this(context)
        {
            foreach (var item in violations)
                Errors.Add(item);
        }
        // This is like the setter of the Message property
        public override string Message => this.ToString();

        public override string ToString()
        {
            string message = "Business rule violation for the following: ";
            var errorMessages = Errors.Select(x => x.Message).ToList();
            message += $"{string.Join(", ", errorMessages)}";
            return message;
        }
    }
}
