using System.Collections.Generic;
using WPFPractice.BindingEnums;

namespace WPFPractice.Model
{
    
    public class Parameter
    {
        /// <summary>
        /// Name is a name of parameter
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// SelectedParameterType is a type in a combobox, a default value
        /// </summary>
        public ParameterType SelectedParameterType { get; set; } = ParameterType.SimpleString;
        public List<string> Strings { get; set; }
    }

    //Rem:
    //- комментарии
}
