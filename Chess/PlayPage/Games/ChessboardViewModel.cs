using Library.Boards;
using Library.Boards.BoardNS;
using Library.GameElems;
using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFTestChess.HelpElems;
using WPFTestChess.PlayPage.ViewModels;

namespace WPFTestChess.PlayPage.Games
{
    internal class ChessboardViewModel: MyNotifyPropertyChanged
    {

            #region Commands

            protected RelayCommand click;
            public ICommand Click => click;

            protected RelayCommand startNewGameCmd;
            public ICommand StartNewGameCmd => startNewGameCmd;

            #endregion

            #region Properties

            protected double chessboardHeight;
            public double ChessboardHeight
            {
                get
                {
                    return chessboardHeight <= chessboardWidth ? chessboardHeight : chessboardWidth;
                }
                set
                {
                    chessboardHeight = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PieceLength));
                    OnPropertyChanged(nameof(ChessboardLength));
                    OnPropertyChanged(nameof(ChessboardWidth));
                }
            }

            protected double chessboardWidth;
            public double ChessboardWidth
            {
                get
                {
                    return chessboardHeight <= chessboardWidth ? chessboardHeight : chessboardWidth;
                }
                set
                {
                    chessboardWidth = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PieceLength));
                    OnPropertyChanged(nameof(ChessboardLength));
                    OnPropertyChanged(nameof(ChessboardHeight));
                }
            }


            //protected int chessboardlength;
            public double ChessboardLength
            {
                get
                {
                    return ChessboardHeight <= ChessboardWidth ? ChessboardHeight : ChessboardWidth;
                }
            }

            public double PieceLength
            {
                get
                {
                    return ChessboardLength / Chessboard.CELL_COUNT;
                }
            }

            public bool isClickOnImage
            {
                get
                {
                    return isClickOnImage;
                }
                set
                {
                    isClickOnImage = value;

                }
            }

            protected bool isMouseDown;
            public bool IsMouseDown
            {
                get { return isMouseDown; }
                set
                {
                    isMouseDown = value;
                    OnPropertyChanged();
                }
            }

            //protected Image selectedPiece;
            //public Image SelectedPiece
            //{
                //get { return selectedPiece; }
                //set
                //{
                    //selectedPiece = value;
                    //OnPropertyChanged();
                //}
            //}

            protected Point mousePoint;
            public Point MousePoint
            {
                get
                {
                    Point newPoint = mousePoint;

                    newPoint.X -= (int)(ChessboardLength / Chessboard.CELL_COUNT / 2);
                    newPoint.Y -= (int)(ChessboardLength / Chessboard.CELL_COUNT / 2);

                    return newPoint;
                }
                set
                {
                    mousePoint = value;
                    OnPropertyChanged();
                }
            }
            #endregion

            #region ViewModels


            protected PieceSelectionViewModel piece_Selection_View_Model;
            public PieceSelectionViewModel Piece_Selection_View_Model
            {
                get
                {
                    return piece_Selection_View_Model;
                }
                set
                {
                    piece_Selection_View_Model = value;
                    OnPropertyChanged();
                }
            }

            protected GameOverViewModel gameOverViewModel;
            public GameOverViewModel Game_Over_View_Model
            {
                get
                {
                    return gameOverViewModel;
                }
                set
                {
                    gameOverViewModel = value;
                    OnPropertyChanged();
                }
            }

            #endregion

            public Chessboard chessboard { get; set; }

            protected ObservableCollection<BoardCell> backCellsList = new ObservableCollection<BoardCell>();
            public IEnumerable<BoardCell> BackCells => backCellsList;

            public BoardCell selectedBC;

            protected static PieceColors ourColor;
            public static PieceColors OurColor
            {
                get
                {
                    return ourColor;
                }
                set
                {
                    ourColor = value;
                    //OnPropertyChanged();
                }
            }

            protected bool isCanSelected = true;

            protected void BcChange(BoardCell bc)
            {
                isCanSelected = false;
                selectedBC = bc;
                selectedBC.Piece.Move(Converter.BoardCellsToPieces(ref chessboard.board.board));

                chessboard.board.ClearCircle();
                chessboard.board.ClearPress();

                foreach (var p in selectedBC.Piece.movePointInts)
                {
                    chessboard.board.board[p.Y, p.X].BackCell.IsCircle = true;
                    chessboard.board.board[p.Y, p.X].BackCell.IsAttackCircle = false;
                }

                foreach (var p in selectedBC.Piece.attackPointInts)
                {
                    chessboard.board.board[p.Y, p.X].BackCell.IsCircle = true;
                    chessboard.board.board[p.Y, p.X].BackCell.IsAttackCircle = true;
                }

                selectedBC.BackCell.IsPressedTrue = true;
            }

            


            public bool IsMouseCLick(object obj)
            {
                BoardCell bc = obj as BoardCell;
                return bc.BackCell.IsPressedFalse == true;
            }

            public void PieceSelectionCreate(PieceColors color, Point cPoint)
            {
               // Piece_Selection_View_Model = new PieceSelectionViewModel(color, cPoint, SelectedTypeInPieceSelection);
            }

            public void SelectedTypeInPieceSelection(PieceType pieceType)
            {
                chessboard.ChangePawn(pieceType);
                Piece_Selection_View_Model = null;
            }

            public void GameOver(GameState state)
            {
                Game_Over_View_Model = new GameOverViewModel(state, StartNewGame);
                IsStartNewButtonEnabled = false;
            }
            protected bool isStartNewButtonEnabled = true;
            public bool IsStartNewButtonEnabled
            {
                get { return isStartNewButtonEnabled; }
                set
                {
                    isStartNewButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
            public void StartNewGame()
            {
                chessboard.StartNewGame();
                Game_Over_View_Model = null;
                selectedBC = null;
                IsStartNewButtonEnabled = true;
            }
    }
}
