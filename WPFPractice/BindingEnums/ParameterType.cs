using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPractice.Model;

namespace WPFPractice.BindingEnums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ParameterType
    {
        [Description("Простая строка")]
        SimpleString,
        [Description("Строка с историей")]
        StringWithHistory,
        [Description("Значение из списка")]
        ValueFromList,
        [Description("Набор значений из списка")]
        SetFromList
    }
}
