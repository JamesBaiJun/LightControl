using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightControl_Demo
{
    [POCOViewModel]
    public class MainViewModel
    {
        public void Test(object content)
        {
            Debug.WriteLine(content.ToString());
        }
    }
}
