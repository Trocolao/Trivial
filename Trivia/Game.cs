using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    public class Game
    {
        private readonly List<string> _players = new List<string>();

        private readonly int[] _places = new int[6];
        private readonly int[] _purses = new int[6];

        private readonly bool[] _inPenaltyBox = new bool[6];

        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast(("Rock Question " + i));
            }
        }
        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }
        public bool Add(string playerName)
        {
            _players.Add(playerName);
            _places[HowManyPlayers()] = 0;
            _purses[HowManyPlayers()] = 0;
            _inPenaltyBox[HowManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }
        public int HowManyPlayers()
        {
            return _players.Count;
        }
        public void Roll(int roll)
        {
            Console.WriteLine(_players[_currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");
                    AvanzarCasillas(roll);
                }
                else
                {
                    Console.WriteLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                AvanzarCasillas(roll);
            }
        }
        private void AvanzarCasillas(int roll)
        {
            _places[_currentPlayer] = _places[_currentPlayer] + roll;
            if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

            Console.WriteLine(_players[_currentPlayer]
                    + "'s new location is "
                    + _places[_currentPlayer]);
            Console.WriteLine("The category is " + CurrentCategory());
            AskQuestion();
        }
        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                HacerPreguntaDeUnaCategoria(_popQuestions);
            }
            if (CurrentCategory() == "Science")
            {
                HacerPreguntaDeUnaCategoria(_scienceQuestions);
            }
            if (CurrentCategory() == "Sports")
            {
                HacerPreguntaDeUnaCategoria(_sportsQuestions);
            }
            if (CurrentCategory() == "Rock")
            {
                HacerPreguntaDeUnaCategoria(_rockQuestions);
            }
        }
        public void HacerPreguntaDeUnaCategoria(LinkedList<string> categoria)
        {
            Console.WriteLine(categoria.First());
            categoria.RemoveFirst();
        }
        private string CurrentCategory()
        {
            if (_places[_currentPlayer] == 0|| _places[_currentPlayer] == 4|| _places[_currentPlayer] == 8) return "Pop";
            else if (_places[_currentPlayer] == 1 || _places[_currentPlayer] == 5 || _places[_currentPlayer] == 9) return "Science";
            else if (_places[_currentPlayer] == 2 || _places[_currentPlayer] == 6 || _places[_currentPlayer] == 10) return "Sports";
            return "Rock";
        }
        public bool WasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    return RespuestaCorrecta();
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                return RespuestaCorrecta();
            }
        }
        private bool RespuestaCorrecta()
        {
            Console.WriteLine("Answer was corrent!!!!");
            _purses[_currentPlayer]++;
            Console.WriteLine(_players[_currentPlayer]
                    + " now has "
                    + _purses[_currentPlayer]
                    + " Gold Coins.");

            var winner = DidPlayerWin();
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;

            return winner;
        }
        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }
        private bool DidPlayerWin()
        {
            return !(_purses[_currentPlayer] == 6);
        }
    }
}
