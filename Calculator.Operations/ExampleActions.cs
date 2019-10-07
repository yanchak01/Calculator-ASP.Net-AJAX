using System;
using System.Collections.Generic;

namespace Calculator.Operations
{
    public class ExampleActions:IExampleActions
    {
         private bool IsDelimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
                return true;
            return false;
        }
        private bool IsOperator(char c)
        {
            if (("+-/*^?()").IndexOf(c) != -1)
            {
                return true;
            }
            return false;
        }
         private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                case '?': return 6;
                default: return 7;
            }
        }

         public double Calculate(string input)
        {
            string output = GetExpression(input); //Перетворення виразу в постфіксний запис
            double result = Counting(output); //Обрахувуєм отриманий вираз
            return result; 
        }

         private string GetExpression(string input)
        {
            string output = string.Empty; //Строка для збереження виразу
            Stack<char> operStack = new Stack<char>(); //Стек для збереження операторів

            for (int i = 0; i < input.Length; i++) //Для кожного символу у вхідній строці
            {
                //Якщо символ - цифра, то зчитую все число
                if (Char.IsDigit(input[i])||input[i]=='.') //Якщо цифра
                {
                    //Читаєм до розділителя або оператора, щоб отримати число
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i]; //Додаю кожну цифру числа до строки
                        i++; //Перехожу до наступного символу

                        if (i == input.Length) break; //Якщо символ - останній, то вихожу з циклу
                    }

                    output += " "; //Дописую після числа пробіл в строку з виразом
                    i--; //Вертаюсь на один символ назад, до символу перед розділителя
                }

                //Якщо символ - оператор
                if (IsOperator(input[i])) //Якщо оператор
                {
                    if (input[i] == '(') //Якщо символ - відкриваюча дужка
                        operStack.Push(input[i]); //Записую її в стек
                    else if (input[i] == ')') //Якщо символ - закрываюча дужка
                    {
                        //Виписую всі оператори до відкриваючої дужки в строку
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else //Якщо любий другий оператор
                    {
                        if (operStack.Count > 0) //Якщо в стекови є елементи
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) //І якщо приіорітет оператора менше або рівний пріорітету оператора на вершині стеку
                                output += operStack.Pop().ToString() + " "; //То добавляем останній оператор з стека в строку с виразом

                        operStack.Push(char.Parse(input[i].ToString())); //Якщо стек пустий, або приіоритет оператора выще - добавляю оператор на вершину стеку

                    }
                }
            }

            //Коли пройшли по всім символам, викидаю з стеку всі оператори, що там залишились в строку
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output; //Поаертаю вираз в постфіксній формі
        }
        private double Counting(string input)
        {
            double result = 0; //Результат
            Stack<double> temp = new Stack<double>(); //стек для рішення

            for (int i = 0; i < input.Length; i++) //Для кожного символу в строці
            {
                //Якщо символ - цифра, то читаю всі числа і записую на вершину стеку
                if (Char.IsDigit(input[i])||input[i]=='.')
                {
                    string a = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i])) //Поки не розділитель
                    {
                        a += input[i]; //Додаємо
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(a)); //Записую в стек
                    i--;
                }
                else if (IsOperator(input[i])) //Якщо символ - оператор
                {
                    //Беремо два останніх значення з стеку
                    double a = temp.Pop();
                    double b = temp.Pop();

                    switch (input[i]) //І роблю над ними дії, згідно оператору
                    {
                        case '+': result = b + a; break;
                        case '-': result = b - a; break;
                        case '*': result = b * a; break;
                        case '/': result = b / a; break;
                        case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                        case '?': result = double.Parse(Math.Sqrt(double.Parse(a.ToString())).ToString());break;
                    }
                    temp.Push(result); //Результат записую назад в стек
                }
            }
            return temp.Peek(); //Забираю результат всіх виразів з стеку и повертаю його
        }
    }
}
