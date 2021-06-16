using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPFPractice.BindingEnums;

namespace WPFPractice.Model
{
    
    public class Parameter
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A parameter type
        /// </summary>
        public ParameterType ParameterType { get; set; } = ParameterType.SimpleString;
        /// <summary>
        /// A list of string values
        /// </summary>
        public ObservableCollection<string> ValuesList { get; set; }
    }
    //Rem: [solved] комментарии
}
