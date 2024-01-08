using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightControl.Controls;

namespace LightControl_Demo
{
    [POCOViewModel]
    public class MainViewModel
    {
        public void Test(object content)
        {
            FlyoutNotification.ShowNotication(content.ToString(), "标题");
        }
    }
}
