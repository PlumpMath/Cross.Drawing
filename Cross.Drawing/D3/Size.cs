﻿using System;

namespace Cross.Drawing.D3
{
    /// <summary>
    /// Size - A value type which defined a size in terms of non-negative width and height
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// Constructor which sets the size's initial values.  Width and Height must be non-negative
        /// </summary>
        /// <param name="width"> double - The initial Width </param>
        /// <param name="height"> double - THe initial Height </param>
        public Size(double width, double height)
        {
            if (width < 0 || height < 0)
            {
                throw new System.ArgumentException("Size_WidthAndHeightCannotBeNegative");
            }

            _width = width;
            _height = height;
        }

        /// <summary>
        /// Empty - a static property which provides an Empty size.  Width and Height are
        /// negative-infinity.  This is the only situation
        /// where size can be negative.
        /// </summary>
        public static Size Empty
        {
            get
            {
                return s_empty;
            }
        }

        /// <summary>
        /// IsEmpty - this returns true if this size is the Empty size.
        /// Note: If size is 0 this Size still contains a 0 or 1 dimensional set
        /// of points, so this method should not be used to check for 0 area.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return _width < 0;
            }
        }

        /// <summary>
        /// Width - Default is 0, must be non-negative
        /// </summary>
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (IsEmpty)
                {
                    throw new System.InvalidOperationException("Size_CannotModifyEmptySize");
                }

                if (value < 0)
                {
                    throw new System.ArgumentException("Size_WidthCannotBeNegative");
                }

                _width = value;
            }
        }

        /// <summary>
        /// Height - Default is 0, must be non-negative.
        /// </summary>
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (IsEmpty)
                {
                    throw new System.InvalidOperationException("Size_CannotModifyEmptySize");
                }

                if (value < 0)
                {
                    throw new System.ArgumentException("Size_HeightCannotBeNegative");
                }

                _height = value;
            }
        }

        /// <summary>
        /// Explicit conversion to Vector.
        /// </summary>
        /// <returns>
        /// Vector - A Vector equal to this Size
        /// </returns>
        /// <param name="size"> Size - the Size to convert to a Vector </param>
        public static explicit operator Vector(Size size)
        {
            return new Vector(size._width, size._height);
        }

        /// <summary>
        /// Explicit conversion to Point
        /// </summary>
        /// <returns>
        /// Point - A Point equal to this Size
        /// </returns>
        /// <param name="size"> Size - the Size to convert to a Point </param>
        public static explicit operator Point(Size size)
        {
            return new Point(size._width, size._height);
        }

        static private Size CreateEmptySize()
        {
            Size size = new Size();
            // We can't set these via the property setters because negatives widths
            // are rejected in those APIs.
            size._width = Double.NegativeInfinity;
            size._height = Double.NegativeInfinity;
            return size;
        }

        private readonly static Size s_empty = CreateEmptySize();

        /// <summary>
        /// Compares two Size instances for exact equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two Size instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='size1'>The first Size to compare</param>
        /// <param name='size2'>The second Size to compare</param>
        public static bool operator ==(Size size1, Size size2)
        {
            return size1.Width == size2.Width &&
            size1.Height == size2.Height;
        }

        /// <summary>
        /// Compares two Size instances for exact inequality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which are logically equal may fail.
        /// Furthermore, using this equality operator, Double.NaN is not equal to itself.
        /// </summary>
        /// <returns>
        /// bool - true if the two Size instances are exactly unequal, false otherwise
        /// </returns>
        /// <param name='size1'>The first Size to compare</param>
        /// <param name='size2'>The second Size to compare</param>
        public static bool operator !=(Size size1, Size size2)
        {
            return !(size1 == size2);
        }
        /// <summary>
        /// Compares two Size instances for object equality.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the two Size instances are exactly equal, false otherwise
        /// </returns>
        /// <param name='size1'>The first Size to compare</param>
        /// <param name='size2'>The second Size to compare</param>
        public static bool Equals(Size size1, Size size2)
        {
            if (size1.IsEmpty)
            {
                return size2.IsEmpty;
            }
            else
            {
                return size1.Width.Equals(size2.Width) &&
                size1.Height.Equals(size2.Height);
            }
        }

        /// <summary>
        /// Equals - compares this Size with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if the object is an instance of Size and if it's equal to "this".
        /// </returns>
        /// <param name='o'>The object to compare to "this"</param>
        public override bool Equals(object o)
        {
            if ((null == o) || !(o is Size))
            {
                return false;
            }

            Size value = (Size)o;
            return Size.Equals(this, value);
        }

        /// <summary>
        /// Equals - compares this Size with the passed in object.  In this equality
        /// Double.NaN is equal to itself, unlike in numeric equality.
        /// Note that double values can acquire error when operated upon, such that
        /// an exact comparison between two values which
        /// are logically equal may fail.
        /// </summary>
        /// <returns>
        /// bool - true if "value" is equal to "this".
        /// </returns>
        /// <param name='value'>The Size to compare to "this"</param>
        public bool Equals(Size value)
        {
            return Size.Equals(this, value);
        }
        /// <summary>
        /// Returns the HashCode for this Size
        /// </summary>
        /// <returns>
        /// int - the HashCode for this Size
        /// </returns>
        public override int GetHashCode()
        {
            if (IsEmpty)
            {
                return 0;
            }
            else
            {
                // Perform field-by-field XOR of HashCodes
                return Width.GetHashCode() ^
                Height.GetHashCode();
            }
        }

        /// <summary>
        /// Creates a string representation of this object based on the current culture.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ToString()
        {
            if (IsEmpty)
            {
                return "Empty";
            }

            return String.Format("{0},{2}", _width, _height);
        }

        internal double _width;
        internal double _height;

    }
}

