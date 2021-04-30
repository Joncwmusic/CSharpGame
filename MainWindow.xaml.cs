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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        // Holds Current Results of Cell
        private MarkType[] mResults;  //Decalre a var of type "MarkType"
        private bool mPlayer1Turn; //Declare bool for which turn it is
        private bool mGameEnded; //Declare bool for when the game ends
        
        #endregion

        #region Constructor
        // Default Constructor

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        #endregion

        private void NewGame() 
        {
            mResults = new MarkType[9]; //create an array of 9 cells
            for (var i = 0; i < mResults.Length; i++) {
                mResults[i] = MarkType.Free;
            }

            mPlayer1Turn = true;


            //All the children of the Grid Container have been cast to an enum of buttons
            //Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button => {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;
        }


        //sender is thing being clicked
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            //Check Game End
            if (mGameEnded) {
                NewGame();
                return;
            }


            //Cast sender to a button
            //Find the buttons in the array
            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + (row * 3);

            if (mResults[index] != MarkType.Free) {
                return;
            }

            //Set the cell value based on player turn
            if (mPlayer1Turn) {
                mResults[index] = MarkType.Cross;
            } else {
                mResults[index] = MarkType.Circle;
            }

            //Show the button content
            if (mPlayer1Turn) {
                button.Content = "X";
            } else {
                button.Content = "O";
            }

            //Change Os to green
            if (!mPlayer1Turn) {
                button.Foreground = Brushes.Red;
            }

            //swap turns
            // This toggles booleans mPlayer1Turn ^= true;
            if (mPlayer1Turn) {
                mPlayer1Turn = false;
            } else {
                mPlayer1Turn = true;
            }

            CheckForWinner();
        }

        private void CheckForWinner() 
        {
            #region Horizontal Wins
            //Check for Horizontal Wins

            var same0 = (mResults[1] & mResults[2]) == mResults[0];
            var same1 = (mResults[4] & mResults[5]) == mResults[3];
            var same2 = (mResults[7] & mResults[8]) == mResults[6];

            //Row 0
            if (mResults[0] != MarkType.Free && same0) {
                mGameEnded = true;
                //highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //Row 1
            if (mResults[3] != MarkType.Free && same1) {
                mGameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //Row 2 
            if (mResults[6] != MarkType.Free && same2) {
                mGameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical Wins
            //Check for Vertical Wins
            var same3 = (mResults[3] & mResults[6]) == mResults[0];
            var same4 = (mResults[4] & mResults[7]) == mResults[1];
            var same5 = (mResults[5] & mResults[8]) == mResults[2];

            //Column 1
            if (mResults[0] != MarkType.Free && same3) {
                mGameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //Column 2
            if (mResults[1] != MarkType.Free && same4) {
                mGameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //Column 2
            if (mResults[1] != MarkType.Free && same4) {
                mGameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Diagonal Wins
            //Check for Diagonal Wins
            var same6 = (mResults[4] & mResults[8]) == mResults[0];
            var same7 = (mResults[4] & mResults[6]) == mResults[2];

            if (mResults[0] != MarkType.Free && same6) {
                mGameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            if (mResults[2] != MarkType.Free && same7) {
                mGameEnded = true;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            #endregion


            //Tie Game
            if (!mResults.Any(result => result == MarkType.Free) & (mGameEnded = false)) { //if all results are not free, there are no moves
                Container.Children.Cast<Button>().ToList().ForEach(button => {
                    button.Background = Brushes.Orange;
                });
            }
        }
    }
}
