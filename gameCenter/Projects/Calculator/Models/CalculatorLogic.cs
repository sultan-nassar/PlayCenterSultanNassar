using System;
using System.Windows;

namespace gameCenter.Projects.Calculator.Models
{
    class CalculatorLogic
    {
        
            private double temp = 0;
            private string operation = "";
            private string output = "";

            public string Output
            {
                get { return output; }
            }

            public void HandleNumBtnClick(string digit)
            {
                output += digit;
            }


        public void HandleDecimalBtnClick()
        {
            if (!output.Contains('.'))
            {
                output += '.';
            }
           
        }
    
        public void HandleOperatorBtnClick(string operatorName)
            {
                if (output != "")
                {
                try
                {
                    temp = double.Parse(output);
                    output = "";
                    operation = operatorName;
                }
                catch
                {
                    MessageBox.Show("you can not do this operation");
                }
                 
                }

            }

            public void HandleToggleplusMinusBtnClick()
            {
                if (!string.IsNullOrEmpty(output))
                {
                    double currentValue = double.Parse(output);
                    currentValue *= -1;
                    output = currentValue.ToString();
                }
            }

            public void HandleEqualBtnClick()
            {
                double outputTemp;

                switch (operation)
                {

                //חיסור
                case "-":
                    if (!string.IsNullOrEmpty(output))
                    {
                        double operand;
                        if (double.TryParse(output, out operand))
                        {
                            outputTemp = temp - operand;
                            output = outputTemp.ToString();
                        }
                        else
                        {
                            // Handle invalid input, e.g., display an error message
                            output = "Error";
                            MessageBox.Show("Invalid input for Minus");
                        }
                    }
                    break;

                    //הוספה
                case "+":
                    if (!string.IsNullOrEmpty(output))
                    {
                        double operand;
                        if (double.TryParse(output, out operand))
                        {
                            outputTemp = temp + operand;
                            output = outputTemp.ToString();
                        }
                        else
                        {
                            // Handle invalid input, e.g., display an error message
                            output = "Error";
                            MessageBox.Show("Invalid input for addition");
                        }
                    }
                    break;

                    //כפל
                case "*":
                    if (!string.IsNullOrEmpty(output))
                    {
                        double operand;
                        if (double.TryParse(output, out operand))
                        {
                            outputTemp = temp * operand;
                            output = outputTemp.ToString();
                        }
                        else
                        {
                            // Handle invalid input, e.g., display an error message
                            output = "Error";
                            MessageBox.Show("Invalid input for Multiply");
                        }
                    }
                    break;

                    //חילוק
                case "\u00F7":
                    if (!string.IsNullOrEmpty(output))
                    {
                        if (double.TryParse(output, out double divisor))
                        {
                            if (divisor != 0)
                            {
                                outputTemp = temp / divisor;
                                output = outputTemp.ToString();
                            }
                            else
                            {
                                // Handle division by zero error
                                output = "Error";
                                MessageBox.Show("You cannot divide by zero!");
                            }
                        }
                        else
                        {
                            // Handle invalid input (non-numeric characters)
                            output = "Error";
                            MessageBox.Show("Invalid input for division!");
                        }
                    }
                    break;



                    //ריבוע 
                case "X" + "\u00B2":
                        outputTemp = Math.Pow(temp, 2);
                        output = outputTemp.ToString();
                        break;

                    //שורש
                    case "√":
                        if (temp >= 0)
                        {
                            outputTemp = Math.Sqrt(temp);
                            output = outputTemp.ToString();
                        }
                        else
                        {
                            // Handle square root of negative number error
                            output = "Error";
                        }
                        break;
                }
            }

            public void HandleClearBtnClick()
            {
                output = "";
                temp = 0;
                operation = "";
            }
        }
    }
