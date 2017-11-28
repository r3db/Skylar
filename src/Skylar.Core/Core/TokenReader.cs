using System;
using System.IO;
using Epiphany.Extensions;

namespace Skylar
{
    internal sealed class TokenReader
    {
        #region Internal Instance Data

        private readonly string _content;
        private int _index;

        #endregion

        #region .Ctor

        internal TokenReader(string content)
        {
            _content = content;
            Line = 1;
            Column = 1;
        }

        #endregion

        #region Auto Implemented Properties

        internal int Line { get; private set; }

        internal int Column { get; private set; }

        #endregion

        #region Properties

        internal char Current
        {
            get
            {
                return _content[_index];
            }
        }

        #endregion

        #region Methods

        #region CanMoveForward

        internal bool CanMoveForward()
        {
            return CanMoveForward(1);
        }

        internal bool CanMoveForward(int length)
        {
            return CanRead(length + 1);
        }

        #endregion

        #region CanRead

        internal bool CanRead(int length)
        {
            return _index + length - 1 < _content.Length;
        }

        #endregion

        #region Peek

        internal string Peek()
        {
            return Peek(1);
        }

        internal string Peek(int length)
        {
            if (CanRead(length) == false)
            {
                throw new EndOfStreamException("Cannot read beyond end of the stream.");
            }

            return _content.Substring(_index, length);
        }

        #endregion

        #region Read

        internal string Read()
        {
            return Read(1);
        }

        internal string Read(int length)
        {
            var result = Peek(length);
            Move(length);
            return result;
        }

        #endregion

        #region Consume

        internal void ConsumeNegligible()
        {
            while (CanRead(1))
            {
                if (Current.IsWhiteSpaceOrControl())
                {
                    Move();
                }
                else
                {
                    break;
                }
            }
        }

        #endregion

        #region IsMatch

        internal bool IsMatch(string value)
        {
            return CanRead(value.Length) && Peek(value.Length).OrdinalEquals(value);
        }

        #endregion

        #region Move

        internal void Move()
        {
            Move(1);
        }

        internal void Move(int length)
        {
            if (CanRead(length) == false)
            {
                throw new EndOfStreamException("Cannot read beyond end of the stream.");
            }

            for (int i = 0; i < length; ++i)
            {
                ++Column;

                var ri = _index + i;

                if (_content[ri] == '\n')
                {
                    Line++;
                    Column = 1;
                }
            }

            _index += length;
        }

        #endregion

        #region Clone

        internal TokenReader Clone()
        {
            return new TokenReader(_content)
            {
                _index = _index,
                Line = Line,
                Column = Column,
            };
        }

        #endregion

        #endregion
    }
}