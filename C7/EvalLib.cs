using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace C7
{   
    class EvalLib
    {
        private string sb = null;
        private bool canUnaryMinus = true;

        public double TextToValue(string text)
        {
            int l = text.Length;
            polish(text);
            double p = Eval_polish();
            return p;
        }

        private void polish(string text)
        {
            char[] stact = new char[30];///stack used for operator storing.
            int top = 1, i = 0;
            stact[0] = '(';  ///initially push ( to stack
            text = text + ")";
            while (top != 0)
            {
                string a = null; ///a variable for storing operands
                if ((text[i] > 47 && text[i] < 58) || text[i] == '.')
                {
                    try
                    {
                        while ((text[i] > 47 && text[i] < 58) || text[i] == '.')
                        {
                            a += text[i].ToString();

                            i++;
                        }
                        sb += (a.ToString());
                        sb += ","; ///sentinell to seperate operands
                        canUnaryMinus = false;
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
                else if (text[i] == '+' || text[i] == '-' || text[i] == '*' || text[i] == '/' || text[i] == '%' || text[i] == '!')
                {
                    int p = Precedance(stact[top - 1]), q = Precedance(text[i]);
                    while (p >= q)
                    {
                        top--;
                        sb += (stact[top].ToString());
                        sb += ",";

                        p = Precedance(stact[top - 1]);
                    }
                    if (q == 5)
                        stact[top] = 'u';/// u represents unary minus.
                    else
                        stact[top] = text[i];
                    i++;
                    top++;
                    canUnaryMinus = true;
                }
                else if (text[i] == '(')
                {
                    stact[top] = text[i];
                    top++;
                    i++;
                }
                else if (text[i] == ')')
                {
                    while (stact[top - 1] != '(')
                    {
                        top--;
                        sb += (stact[top].ToString());
                        sb += ",";
                    }
                    top--;
                    i++;
                }
                else return;
            }

        }

        private int Precedance(char a)///returns operator precedance
        {
            if (canUnaryMinus && a == '-')
            {
                canUnaryMinus = false;
                return 5;
            }
            int precedenceNumber = 0;
            switch (a)
            {
                case '+':
                case '-':
                    precedenceNumber = 1;
                    break;
                case '*':
                case '/':
                    precedenceNumber = 2;
                    break;
                case '%':
                    precedenceNumber = 3;
                    break;
                case '!':
                    precedenceNumber = 4;
                    break;
                case 'u':
                    precedenceNumber = 5;
                    break;
            }
            return precedenceNumber;
        }

        private double Eval_polish()
        {
            double[] stact = new double[32];
            int top = 0, length = sb.Length - 1, i = 0;
            try
            {
                while (i < length)
                {
                    string s = null;
                    again:
                    if (sb[i] == ',')
                    {
                        i++;
                    }
                    else if (sb[i] > 47 && sb[i] < 58 || sb[i] == '.')
                    {
                        s += sb[i].ToString();
                        i++;
                        goto again;
                    }
                    if (s != null)
                    {
                        stact[top] = double.Parse(s);
                        top++;
                        s = null;
                    }
                    if (sb[i] == '+' || sb[i] == '-' || sb[i] == '*' || sb[i] == '/' || sb[i] == '%')
                    {

                        double a, b;
                        top--;
                        a = stact[top];
                        top--;
                        b = stact[top];

                        switch (sb[i])
                        {
                            case '+':
                                a += b;
                                break;
                            case '-':
                                a = b - a;
                                break;
                            case '*':
                                a = b * a;
                                break;
                            case '/':
                                a = b / a;
                                break;
                            case '%':
                                a = b % a;
                                break;
                        }
                        stact[top] = a;
                        top++;
                        i++;
                    }
                    else if (sb[i] == 'u' || sb[i] == '!')
                    {
                        double a, b = 1;
                        top--;
                        a = stact[top];
                        if (sb[i] == 'u')
                        {
                            a = -a;
                        }
                        else
                        {
                            if (a > 170)
                            {

                                System.Windows.Forms.MessageBox.Show("Too Big for factorialization.\nIt can't be more then 170.", "Error!");
                                return 0;
                            }
                            for (double mul = 1; mul <= a; mul++)
                            {
                                b *= mul;
                            }
                            a = b;
                        }
                        stact[top] = a;
                        top++;
                        i++;
                    }

                }

                return stact[top - 1];
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error");
                return 0;
            }
        }

    }
}
