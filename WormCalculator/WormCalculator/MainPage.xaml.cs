using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;

namespace WormCalculator
{
	public partial class MainPage : ContentPage
	{
		private string displayString = "";

		private double inputNum = 0d;
		private double firstNum = 0d;
		private double answer = 0d;
		private double secondNum = 0d;

		private char inputSymbol;
		private char operationSymbol = 'Q';

		private int numberTaps = 0;

		private bool updateFirstNum = true;

		public MainPage()
		{
			InitializeComponent();
			BindingContext = this;
		}

		//Used to display the text to the user
		public string DisplayNumber
		{
			get { return displayString; }

			set
			{
				//Copy this to update the display text
				OnPropertyChanged(nameof(DisplayNumber));
			}
		}

		//Handles the input from the number buttons
		void HandleNumberButtons(object sender, System.EventArgs e)
		{
			inputNum = double.Parse(((Button)sender).Text.ElementAt(0).ToString());

			if (updateFirstNum && numberTaps == 0)
			{
				displayString = "";
			}

			numberTaps++;

			HandlingNumbers(inputNum);
		}

		//Handles the input from the symbol buttons
		void HandleSymbolButtons(object sender, System.EventArgs e)
		{
			inputSymbol = ((Button)sender).Text.ElementAt(0);

			if(inputSymbol == '=' && operationSymbol != 'Q')
			{
				updateFirstNum = true;
				CompleteOperation();
			}
			else if(inputSymbol != '=')
			{
				updateFirstNum = false;
				operationSymbol = inputSymbol;

				if(numberTaps == 0)
				{
					firstNum = answer;
				}

				HandlingDisplay(" " + inputSymbol.ToString() + " ");
			}
		}

		//Handles what the numbers to be used in the operation are
		private void HandlingNumbers(double num)
		{
			if (updateFirstNum)
			{
				firstNum = (firstNum * 10) + num;
			}
			else
			{
				secondNum = (secondNum * 10) + num;
			}

			HandlingDisplay(num.ToString());
		}

		//Handles modifying the display string through concatenation
		private void HandlingDisplay(string character)
		{
			displayString = displayString + character;

			OnPropertyChanged(nameof(DisplayNumber));
		}

		//Handles the operation based on the symbol selected
		private void CompleteOperation()
		{
			switch (operationSymbol)
			{
				case '+':
					answer = Add();
					break;
			}

			displayString = answer.ToString();
			OnPropertyChanged(nameof(DisplayNumber));

			Reset();
		}

		//Resets the values to be used again in another calculation
		private void Reset()
		{
			operationSymbol = 'Q';
			firstNum = 0;
			secondNum = 0;
			numberTaps = 0;
		}

		//Adds the two numbers
		private double Add()
		{
			return firstNum + secondNum;
		}

		//Subtracts the two numbers
		private double Subtract()
		{
			return firstNum - secondNum;
		}
	}
}
