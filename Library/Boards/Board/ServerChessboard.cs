using Library.Boards;
using Library.Boards.AttackBoardNS;
using Library.Boards.BoardNS;
using Library.GameElems;
using Library.HelpElems;
using Library.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.Boards.BoardNS
{
    public class ServerChessboard:Board
    {
        AttackBoard attackBoard;
        public Piece[,] board = new Piece[Chessboard.CELL_COUNT, Chessboard.CELL_COUNT];
        public Piece[,] checkBoard = new Piece[Chessboard.CELL_COUNT, Chessboard.CELL_COUNT];

        public ServerChessboard()
        {
            InitializePiece(ref board);
            InitializePiece(ref checkBoard);

            gameState = GameState.NoWiners;
            attackBoard = new AttackBoard();

            SelectedColor = PieceColors.White;
        }


        public static PieceColors SelectedColor { get; set; }
        public static void ChangeColor()
        {
            if (SelectedColor == PieceColors.White) SelectedColor = PieceColors.Black;
            else SelectedColor = PieceColors.White;
        }

        public ResaltMsg Roque(Step step)
        {
            attackBoard.PointsCalculation(board);

            if (((King)board[step.CurrentPoint.Y, step.CurrentPoint.X]).isFirstMove == false ||
               ((Rook)board[step.NextPoint.Y, step.NextPoint.X]).isFirstMove == false)
            {
                return new ResaltMsg(false, step, GameState.NoWiners, ResaltType.Roque);
            }

            int x = step.CurrentPoint.X > step.NextPoint.X ? step.NextPoint.X + 1 : step.CurrentPoint.X + 1;
            int max = step.CurrentPoint.X > step.NextPoint.X ? step.CurrentPoint.X : step.NextPoint.X;
            for (; x < max; x++)
            {
                if (board[step.CurrentPoint.Y, x] != null)
                {
                    return new ResaltMsg(false, step, gameState, ResaltType.Roque);
                }
            }
            int moveVector = step.CurrentPoint.X > step.NextPoint.X ? -2 : 2;
            int moveVector2 = step.CurrentPoint.X > step.NextPoint.X ? -1 : 1;
            if (board[step.CurrentPoint.Y, step.CurrentPoint.X].Color == PieceColors.White)
            {
                if (attackBoard.Blackboard[step.CurrentPoint.Y, step.CurrentPoint.X + moveVector].pieces.Count > 0 ||
                    attackBoard.Blackboard[step.CurrentPoint.Y, step.CurrentPoint.X + moveVector2].pieces.Count > 0)
                {
                    return new ResaltMsg(false, step, gameState, ResaltType.Roque);
                }
            }
            else
            {
                if (attackBoard.Whiteboard[step.CurrentPoint.Y, step.CurrentPoint.X + moveVector].pieces.Count > 0 ||
                    attackBoard.Whiteboard[step.CurrentPoint.Y, step.CurrentPoint.X + moveVector2].pieces.Count > 0)
                {
                    return new ResaltMsg(false, step, gameState, ResaltType.Roque);
                }
            }

            RelocatePiece(board, step.CurrentPoint, new PointInt(step.CurrentPoint.X + moveVector, step.CurrentPoint.Y));
            RelocatePiece(board, step.NextPoint, new PointInt(step.CurrentPoint.X + moveVector2, step.CurrentPoint.Y));

            RelocatePiece(checkBoard, step.CurrentPoint, new PointInt(step.CurrentPoint.X + moveVector, step.CurrentPoint.Y));
            RelocatePiece(checkBoard, step.NextPoint, new PointInt(step.CurrentPoint.X + moveVector2, step.CurrentPoint.Y));

            bool f22 = true;
            MoveUpdate(ref f22);

            ((King)board[step.CurrentPoint.Y, step.CurrentPoint.X + moveVector]).isFirstMove = false;
            ((Rook)board[step.NextPoint.Y, step.CurrentPoint.X + moveVector2]).isFirstMove = false;
            return new ResaltMsg(true, step, gameState, ResaltType.Roque);
        }

        public ResaltMsg MovePiece(Step step)
        {
            bool canMove = false;
            if (checkBoard[step.CurrentPoint.Y, step.CurrentPoint.X] != null)
            {
                checkBoard[step.CurrentPoint.Y, step.CurrentPoint.X].Move(checkBoard);

                if (checkBoard[step.CurrentPoint.Y, step.CurrentPoint.X].movePointInts.Contains(step.NextPoint) ||
                   checkBoard[step.CurrentPoint.Y, step.CurrentPoint.X].attackPointInts.Contains(step.NextPoint))
                {
                    RelocatePiece(checkBoard, step.CurrentPoint, step.NextPoint);

                    if (gameState == GameState.NoWiners)
                    {
                        if (board[step.CurrentPoint.Y, step.CurrentPoint.X].Type == PieceType.King)
                        {
                            attackBoard.PointsCalculation(checkBoard);

                            KingState kingState = attackBoard.GetKingState(checkBoard,SelectedColor);

                            if (kingState == KingState.Check)
                            {
                                RegeneratePiece(step.NextPoint, step.CurrentPoint);
                                return new ResaltMsg(canMove, step, gameState, ResaltType.AttackMove);
                            }
                            ((King)board[step.CurrentPoint.Y, step.CurrentPoint.X]).isFirstMove = false;
                        }
                        else if (board[step.CurrentPoint.Y, step.CurrentPoint.X].Type == PieceType.Pawn)
                            ((Pawn)board[step.CurrentPoint.Y, step.CurrentPoint.X]).isFirstMove = false;

                        else if (board[step.CurrentPoint.Y, step.CurrentPoint.X].Type == PieceType.Rook)
                            ((Rook)board[step.CurrentPoint.Y, step.CurrentPoint.X]).isFirstMove = false;

                        canMove = true;

                        RelocatePiece(board, step.CurrentPoint, step.NextPoint);

                        attackBoard.PointsCalculation(checkBoard);

                        //ChangeSelectionColor();
                        ChangeColor();

                        PointInt kingPoint = GetPiecesPoint(board, PieceType.King, SelectedColor)[0];
                        KingState state = attackBoard.GetKingState(checkBoard, SelectedColor);


                        if (state == KingState.Normal)
                        {
                            gameState = GameState.NoWiners;

                        }
                        else if (state == KingState.Check)
                        {
                            List<Piece> attackPieces = SelectedColor == PieceColors.White ? attackBoard.Blackboard[kingPoint.Y, kingPoint.X]?.pieces :
                                                                                                       attackBoard.Whiteboard[kingPoint.Y, kingPoint.X]?.pieces;

                            bool flag = isCheckMate(attackPieces);

                            if (flag == false)
                            {
                                gameState = SelectedColor == PieceColors.White ? GameState.WhiteCheck : GameState.BlackCheck;
                            }
                            else
                            {
                                gameState = SelectedColor == PieceColors.White ? GameState.BlackWin : GameState.WhiteWin;
                                //chessboard.gameOver(gameState);
                            }
                        }

                    }
                    else if (gameState == GameState.BlackCheck || gameState == GameState.WhiteCheck)
                    {
                        attackBoard.PointsCalculation(checkBoard);

                        //ChangeSelectionColor();

                        //PointInt kingPoint = GetPiecesPoint(checkBoard, PieceType.King, Chessboard.SelectedColor)[0];
                        KingState state = attackBoard.GetKingState(checkBoard, SelectedColor);

                        if (state == KingState.Normal)
                        {
                            if (board[step.CurrentPoint.Y, step.CurrentPoint.X].Type == PieceType.Pawn)
                                ((Pawn)board[step.CurrentPoint.Y, step.CurrentPoint.X]).isFirstMove = false;

                            RelocatePiece(board, step.CurrentPoint, step.NextPoint);

                            gameState = GameState.NoWiners;

                            ChangeColor();

                            canMove = true;
                        }
                        else
                        {
                            RegeneratePiece(step.NextPoint, step.CurrentPoint);
                        }
                    }
                }
            }

            return new ResaltMsg(canMove, step, gameState, ResaltType.AttackMove);
        }

        public void MoveUpdate(ref bool isCanSelected)
        {
            attackBoard.PointsCalculation(checkBoard);

            //ChangeSelectionColor();
            ChangeColor();

            PointInt kingPoint = GetPiecesPoint(board, PieceType.King, SelectedColor)[0];
            KingState state = attackBoard.GetKingState(checkBoard, SelectedColor);


            if (state == KingState.Normal)
            {
                gameState = GameState.NoWiners;

                isCanSelected = true;
            }
            else if (state == KingState.Check)
            {
                List<Piece> attackPieces = SelectedColor == PieceColors.White ? attackBoard.Blackboard[kingPoint.Y, kingPoint.X]?.pieces :
                                                                                           attackBoard.Whiteboard[kingPoint.Y, kingPoint.X]?.pieces;

                bool flag = isCheckMate(attackPieces);

                if (flag == false)
                {
                    isCanSelected = true;

                    gameState = SelectedColor == PieceColors.White ? GameState.WhiteCheck : GameState.BlackCheck;
                }
                else
                {
                    gameState = SelectedColor == PieceColors.White ? GameState.BlackWin : GameState.WhiteWin;
                    MessageBox.Show(gameState + "");
                }
            }
        }

        public bool isCheckMate(List<Piece> pieces)
        {
            foreach (var piece in pieces)
            {
                if (piece.Type != PieceType.King)
                {
                    for (int mP = 0; mP < piece.movePointInts.Count; mP++)
                    {
                        var movePoint = piece.movePointInts[mP];
                        bool flag = isDefense(movePoint);

                        if (flag) return false;
                    }

                    var point = piece.ChessboardPointInt;

                    if (isDefense(point)) return false;
                }
                return KingDefense();
            }
            return true;
        }

        public bool KingDefense()
        {
            PointInt kingPoint = GetPiecesPoint(board, PieceType.King, SelectedColor)[0];

            checkBoard[kingPoint.Y, kingPoint.X].Move(checkBoard);

            for (int i = 0; i < checkBoard[kingPoint.Y, kingPoint.X].movePointInts.Count; i++)
            {
                var p = checkBoard[kingPoint.Y, kingPoint.X].movePointInts[i];

                var flag = IsRelocateKing(kingPoint, p);
                if (flag) return false;
            }


            for (int i = 0; i < checkBoard[kingPoint.Y, kingPoint.X].attackPointInts.Count; i++)
            {
                var p = checkBoard[kingPoint.Y, kingPoint.X].attackPointInts[i];

                var flag = IsRelocateKing(kingPoint, p);
                if (flag) return false;
            }
            return true;
        }

        private bool IsRelocateKing(PointInt kingPoint, PointInt nextPoint)
        {
            RelocatePiece(checkBoard, kingPoint, nextPoint);

            attackBoard.PointsCalculation(checkBoard);

            KingState stateRes = attackBoard.GetKingState(checkBoard, SelectedColor);

            RegeneratePiece(nextPoint, kingPoint);

            attackBoard.PointsCalculation(checkBoard);

            if (stateRes == KingState.Normal) return true;
            else attackBoard.PointsCalculation(board);
            return false;
        }
        public bool isDefense(PointInt point)
        {
            attackBoard.PointsCalculation(checkBoard);

            if ((SelectedColor == PieceColors.White &&
                        attackBoard.Whiteboard[point.Y, point.X]?.pieces?.Count > 0)
                     || (SelectedColor == PieceColors.Black &&
                        attackBoard.Blackboard[point.Y, point.X]?.pieces?.Count > 0))
            {
                Piece defensePiece = SelectedColor == PieceColors.White ? attackBoard.Whiteboard[point.Y, point.X]?.pieces?[0] :
                                                                        attackBoard.Blackboard[point.Y, point.X]?.pieces?[0];

                if (defensePiece != null)
                {
                    PointInt point2 = defensePiece.ChessboardPointInt;
                    RelocatePiece(checkBoard, point2, point);

                    attackBoard.PointsCalculation(checkBoard);

                    KingState stateRes = attackBoard.GetKingState(checkBoard, SelectedColor);

                    RegeneratePiece(point, point2);

                    attackBoard.PointsCalculation(checkBoard);

                    if (stateRes == KingState.Normal) return true;
                    //else attackBoard.PointsCalculation(board);
                }
            }
            return false;
        }




        public void RegeneratePiece(PointInt currentPoint, PointInt nextPoint)
        {
            RelocatePiece(checkBoard, currentPoint, nextPoint);
            checkBoard[currentPoint.Y, currentPoint.X] = board[currentPoint.Y, currentPoint.X];
        }
        public void RegenOneCell(PointInt point)
        {
            checkBoard[point.Y, point.X] = board[point.Y, point.X];
        }

        public GameState gameState { get; set; }
    }
}
