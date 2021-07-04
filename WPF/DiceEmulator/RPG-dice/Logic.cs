using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_dice
{
    class Logic
    {

        /// <summary>
        /// Método default para quando uma Text Box tem o foco do teclado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //Altera o alinhamento de texto da Text Box atual
            ((TextBox)sender).TextAlignment = TextAlignment.Left;
        }

        /// <summary>
        /// Método default para quando uma Text Box perde o foco do teclado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //Pega a Text Box atual
            var tBox = (TextBox)sender;

            //Muda o alinhamento do texto
            tBox.TextAlignment = TextAlignment.Center;

            //Inicializa a saída do TryParse
            var OP = new int();

            //Checa se o que está dentro da Text Box é texto e se sim, se é menor que 0 OU
            //Se a Text Box está vazia
            if ((int.TryParse(tBox.Text, out OP) && OP < 0) || tBox.Text == string.Empty)
                //Define texto como tag
                tBox.Text = (string)tBox.Tag;
        }

        /// <summary>
        /// Método default para quando o texto de uma Text Box é alterado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Pega Text Box atual
            var tBox = (TextBox)sender;

            //Inicializa a saída do TryParse
            var num = new int();

            //Se não houver texto...
            if (tBox.Text.Length < 1)
                return;

            /*Em relação ao texto atual...
             * se o último char for + ou - ou , ou . OU
             * se não der pra virar número OU
             * se for 0 E for alguma das caixas de número (valor mínimo = 1) */
            if ("+-,.".Contains(tBox.Text.Last()) ||
                !int.TryParse(tBox.Text.Last().ToString(), out num) ||
                (tBox.Text == "0" && (tBox.Name == "d4Num" || tBox.Name == "d6Num" || tBox.Name == "d12Num" || tBox.Name == "d20Num")))
            {
                //Remova o último char
                tBox.Text = tBox.Text.Remove(tBox.Text.Length - 1, 1);

                //Coloque a seleção atual no final do texto
                tBox.SelectionStart = tBox.Text.Length;
            }
        }

        /// <summary>
        /// Método default para click nos botões de sinal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Se a Window ainda não tiver carregado...
            if (this.IsLoaded == false)
                return;

            //Pega o botão atual
            var but = (Button)sender;

            //Pega a Tag do botão
            switch (int.Parse((string)but.Tag))
            {
                case 4:
                    //Alterna entre + e - no texto do botão
                    d4ModSign.Text = d4ModSign.Text == "+" ? "-" : "+";
                    break;

                case 6:
                    //Idem...
                    d6ModSign.Text = d6ModSign.Text == "+" ? "-" : "+";
                    break;

                case 12:
                    d12ModSign.Text = d12ModSign.Text == "+" ? "-" : "+";
                    break;

                case 20:
                    d20ModSign.Text = d20ModSign.Text == "+" ? "-" : "+";
                    break;
            }

        }

        /// <summary>
        /// Método para click no botão de Reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetBut_Click(object sender, RoutedEventArgs e)
        {
            //Reseta a coluna de número de dados
            d4Num.Text = d6Num.Text = d12Num.Text = d20Num.Text = "1";

            //Reseta a coluna de sinais dos modificadores
            d4ModSign.Text = d6ModSign.Text = d12ModSign.Text = d20ModSign.Text = "+";

            //Reseta a coluna de valores de modificador
            d4Mod.Text = d6Mod.Text = d12Mod.Text = d20Mod.Text = "0";

            //Reseta a coluna de resultados
            d4Res.Text = d6Res.Text = d12Res.Text = d20Res.Text = string.Empty;

            //Reseta o texto do log
            LogText.Text = string.Empty;
        }

        /// <summary>
        /// Método default para botões de Roll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollBut_Click(object sender, RoutedEventArgs e)
        {
            //Instancia novo objeto Random
            var random = new Random();

            //Inicializa a lista de resultados
            var results = new List<int>();

            //Inicializa os resultados
            var res = new int();

            //Inicializa o modificador
            var adder = new int();

            //Pega a tag do botão
            switch (int.Parse((string)((Button)sender).Tag))
            {
                case 4:
                    //Joga o dado N vezes
                    for (int i = 0; i < int.Parse(d4Num.Text); i++)
                    {
                        //Sorteia um inteiro basendo-se nos limites do dado atual
                        results.Add(random.Next(1, 5));
                    }

                    //Se houver modificador...
                    if (d4Mod.Text != "0")
                        adder = int.Parse(d4ModSign.Text + d4Mod.Text);

                    //Pega resultado
                    res = results.Sum() + adder;

                    //Checa se o valor não é menor que 1
                    res = res < 1 ? 1 : res;

                    //Coloca resultado atual na caixa de resultado
                    d4Res.Text = res.ToString();

                    //Escreve no Log
                    LogWrite(results, adder, 4);
                    break;

                case 6:
                    //Idem...
                    for (int i = 0; i < int.Parse(d6Num.Text); i++)
                    {
                        results.Add(random.Next(1, 7));
                    }
                    if (d6Mod.Text != "0")
                        adder = int.Parse(d6ModSign.Text + d6Mod.Text);

                    res = results.Sum() + adder;

                    if (res < 1)
                        res = 1;

                    d6Res.Text = res.ToString();
                    LogWrite(results, adder, 6);
                    break;
                case 12:
                    for (int i = 0; i < int.Parse(d12Num.Text); i++)
                    {
                        results.Add(random.Next(1, 13));
                    }
                    if (d12Mod.Text != "0")
                        adder = int.Parse(d12ModSign.Text + d12Mod.Text);

                    res = results.Sum() + adder;

                    if (res < 1)
                        res = 1;

                    d12Res.Text = res.ToString();
                    LogWrite(results, adder, 12);
                    break;

                case 20:
                    for (int i = 0; i < int.Parse(d20Num.Text); i++)
                    {
                        results.Add(random.Next(1, 21));
                    }
                    if (d20Mod.Text != "0")
                        adder = int.Parse(d20ModSign.Text + d20Mod.Text);

                    res = results.Sum() + adder;

                    if (res < 1)
                        res = 1;

                    d20Res.Text = res.ToString();
                    LogWrite(results, adder, 20);
                    break;
            }
            LogText.ScrollToEnd();
        }

        /// <summary>
        /// Método para escrever resultados no Log
        /// </summary>
        /// <param name="results">Lista de resultados</param>
        /// <param name="adder">Modificador</param>
        /// <param name="dNum">Número do dado</param>
        private void LogWrite(List<int> results, int adder, int dNum)
        {
            //Se o log não estiver vazio...
            if (LogText.Text != string.Empty)
                LogText.Text += "\n\n";

            //Inicializa primeira linha
            var Line1 = $"Rolled die {dNum} - {results.Count} time";

            //Adiciona s para caso plural
            if (results.Count > 1)
                Line1 += "s";

            //Verifica se há modificador
            if (adder != 0)
            {
                //Define sinal do modificador
                var sign = adder > 0 ? "+" : "-";

                //Adiciona à linha
                Line1 += $" ({sign}{Math.Abs(adder).ToString()})";
            }

            //Escreve linha no Log
            LogText.Text += Line1 + ":\n";

            //Se só houver uma jogada...
            if (results.Count == 1)
            {
                //Define resultado
                var res = results[0] + adder;

                //Checa resultado
                res = res < 1 ? 1 : res;

                //Escreve resultado no Log
                LogText.Text += res.ToString();
                return;
            }
            //Caso contrário...

            //Inicializa a contagem de resultados
            var count = 1;

            //Para cada resultado na lista de resultados
            foreach (var result in results)
            {
                //Inicializa a linha atual
                var Line = count.ToString();

                if (count == 1)
                    Line += "st";
                else if (count == 2)
                    Line += "nd";
                else if (count == 3)
                    Line += "rd";
                else
                    Line += "th";

                Line += " roll: ";

                Line += result.ToString();

                //Imprime no Log
                LogText.Text += Line + "\n";

                count++;
            }

            var total = results.Sum() + adder;

            //Checa resultado
            total = total < 1 ? 1 : total;

            //Escreve no Log
            LogText.Text += $"Total = {total}";
        }

        /// <summary>
        /// Método chamado para quando se clica em espaços vazios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Tira o Keyboard Focus de todos os elementos
            Keyboard.ClearFocus();
        }
    }
}
