using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Csharp2_ControlTower;

namespace CSharp2_ControlTower
{
    public class MessageBoxes
    {
        /// <summary>
        /// Displays a messageBox containing information.
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        /// <param name="title">The title of the messagebox</param>
        public static void DisplayInfoBox(string message, string title)
        {
            MessageBox.Show($"{message}",
                $"{title}",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        /// <summary>
        /// Displays a messagebox with an error-message.
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        public static void DisplayErrorBox(string message)
        {
            MessageBox.Show(message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        /// <summary>
        /// Displays a messagebox with a prompt:question that the user can answer yes or no to.
        /// </summary>
        /// <param name="question">The question posed to the user</param>
        /// <param name="title">The title of the messagebox</param>
        /// <returns>True if yes, false if no</returns>
        public static bool DisplayQuestion(string question, string title)
        {
            MessageBoxResult answer = MessageBox.Show(question,
                title,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (answer == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
