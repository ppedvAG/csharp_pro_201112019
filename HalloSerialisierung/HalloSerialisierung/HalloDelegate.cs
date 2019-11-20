using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HalloSerialisierung
{
    delegate void EinfacherDelegate();
    delegate void DeleMitParameter(string txt);
    delegate long CalcDelegate(int a, int b);

    class HalloDelegate
    {
        public HalloDelegate()
        {
            EinfacherDelegate meinDele = EinfacheMethode;
            Action meineDeleAlsAction = EinfacheMethode;
            Action meineDeleAlsActionAno = delegate () { MessageBox.Show("Test"); };
            Action meineDeleAlsActionAno2 = () => { MessageBox.Show("Test"); };
            Action meineDeleAlsActionAno3 = () => MessageBox.Show("Test");

            DeleMitParameter deleMitPara = MethodeMitPara;
            Action<string> deleMitParaAls = MethodeMitPara;
            Action<string> deleMitParaAlsAno = (string txt) => { MessageBox.Show(txt); };
            Action<string> deleMitParaAlsAno2 = (txt) => MessageBox.Show(txt);
            Action<string> deleMitParaAlsAno3 = x => MessageBox.Show(x);

            CalcDelegate calcDele = Sum;
            Func<int, int, long> calcAlsFunc = Sum;
            Func<int, int, long> calcAlsFuncAno = (int a, int b) => { return a + b; };
            Func<int, int, long> calcAlsFuncAno2 = (a, b) => a + b;

            List<string> texte = new List<string>();
            texte.Where(Filter);
            texte.Where(x => x.StartsWith("b"));
        }

        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return !true;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        private void MethodeMitPara(string txt)
        {
            Console.WriteLine(txt);
        }

        public void EinfacheMethode()
        {
            System.Console.WriteLine("Hallo");
        }
    }
}
