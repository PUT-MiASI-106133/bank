using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ninject;
using System.Reflection;

namespace bank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CKIR KIR = new CKIR();

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var bank1 = kernel.Get<IBank>();
            var bank2 = kernel.Get<IBank>();


            KIR.AddBank(bank1);
            KIR.AddBank(bank2);
            tb1.Text = bank1.GetType().ToString();
           // Assert.IsTrue(bank1.GetType() == typeof(CBank)); 

        }
    }
}
