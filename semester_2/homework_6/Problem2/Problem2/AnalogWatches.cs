using System;
using System.Drawing;
using System.Windows.Forms;

namespace WatchesControl
{
    /// <summary>
    /// Implements control for simple analog watches
    /// </summary>
    public class AnalogWatches : Control
    {
        // Some settings

        // Colors
        private readonly Color dialFaceColor = Color.Black;
        private readonly Color hourDivisionColor = Color.Black;
        private readonly Color minuteDivisionColor = Color.Gray;
        private readonly Color hourArrowColor = Color.Black;
        private readonly Color minuteArrowColor = Color.Black;
        private readonly Color secondArrowColor = Color.Red;
        private readonly Color pointOfAttachmentColor = Color.Red;
        private readonly Color backgroundColor = Color.White;

        // Thicknesses (in pixels)
        private const int dialFaceThickness = 7;
        private const int divisionThickness = 2;
        private const int hourArrowThickness = 4;
        private const int minuteArrowThickness = 3;
        private const int secondArrowThickness = 2;

        // Lengths and diameters in parts 
        // (length == <dial face diameter> / part)
        private const int pointOfAttachmentDiameterPart = 40;
        private const int hourArrowLengthPart = 6;
        private const int minuteArrowLengthPart = 4;
        private const int secondArrowLengthPart = 3;
        private const int secondArrowCounterweightPart = 12;
        private const int hourDivisionsLengthPart = 30;
        private const int minuteDivisionsLengthPart = 45;
        private const int hourMultipleOfThreeLengthPart = 15;

        // End settings

        private DateTime currentDateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalogWatches"/> class
        /// with current date and time.
        /// </summary>
        public AnalogWatches()
            : this(DateTime.Now)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalogWatches"/> class
        /// with given date and time.
        /// </summary>
        /// <param name="initialDateTime">Initial date and time</param>
        public AnalogWatches(DateTime initialDateTime)
            : base()
        {
            this.InitializeComponent();

            this.CurrentDateTime = initialDateTime;
        }

        /// <summary>
        /// Gets or sets date and time shown on these watches
        /// </summary>
        public DateTime CurrentDateTime
        {
            get
            {
                return this.currentDateTime;
            }

            set
            {
                this.currentDateTime = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Draws control on the screen
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var dialFaceParameters = this.DrawWatchCircle(e);

            // When moving control in visual constructor
            if (dialFaceParameters.Diameter <= 0)
            {
                return;
            }

            this.DrawAllDivisions(e, dialFaceParameters);
            this.DrawArrows(e, dialFaceParameters);
            this.DrawPointOfAttachment(e, dialFaceParameters);
        }

        /// <summary>
        /// Causes the control to be redrawn when it being resizing
        /// </summary>
        /// <param name="e">Parameters of caller</param>
        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
        }

        /// <summary>
        /// Draws dial face
        /// </summary>
        /// <param name="canvas">Control's canvas</param>
        /// <returns>
        /// Coordinates of the center of the dial face
        /// and it's diameter
        /// </returns>
        protected DialFaceInfo DrawWatchCircle(PaintEventArgs canvas)
        {
            // In pixels
            const int mandatoryPadding = 3;

            var totalPadding = Math.Max(this.Padding.All, 0) + mandatoryPadding;

            var watchDiameter = Math.Min(canvas.ClipRectangle.Height, canvas.ClipRectangle.Width)
                - (2 * totalPadding);

            // When moving control in visual constructor
            if (watchDiameter <= 0)
            {
                return new DialFaceInfo(0, 0, 0);
            }

            var leftTopCornerX = (canvas.ClipRectangle.Width - watchDiameter) / 2;
            var leftTopCornerY = (canvas.ClipRectangle.Height - watchDiameter) / 2;

            var watchPen = new Pen(this.dialFaceColor, dialFaceThickness);
            var watchRectangle = new Rectangle(
                leftTopCornerX,
                leftTopCornerY,
                watchDiameter,
                watchDiameter);

            canvas.Graphics.DrawEllipse(watchPen, watchRectangle);
            canvas.Graphics.FillEllipse(
                new SolidBrush(this.backgroundColor),
                watchRectangle);

            var circleCenterX = leftTopCornerX + (watchDiameter / 2);
            var circleCenterY = leftTopCornerY + (watchDiameter / 2);

            return new DialFaceInfo(
                circleCenterX,
                circleCenterY,
                watchDiameter);
        }

        /// <summary>
        /// Draws all types of divisions on the dial face
        /// </summary>
        /// <param name="canvas">Control's canvas</param>
        /// <param name="dialFace">Info about the dial face</param>
        protected void DrawAllDivisions(PaintEventArgs canvas, DialFaceInfo dialFace)
        {
            // I don't know how to fix arrow overlapping without crutches

            this.DrawDivision(
                canvas,
                dialFace,
                60,
                dialFace.Diameter / minuteDivisionsLengthPart,
                this.minuteDivisionColor);

            // For "12", "3", "6", "9" hours
            this.DrawDivision(
                canvas,
                dialFace,
                4,
                dialFace.Diameter / hourMultipleOfThreeLengthPart,
                this.hourDivisionColor);

            this.DrawDivision(
                canvas,
                dialFace,
                12,
                dialFace.Diameter / hourDivisionsLengthPart,
                this.hourDivisionColor);
        }

        /// <summary>
        /// Draws arrows of the watch
        /// </summary>
        /// <param name="canvas">Control canvas</param>
        /// <param name="dialFace">Parameters of the dial face</param>
        protected void DrawArrows(PaintEventArgs canvas, DialFaceInfo dialFace)
        {
            // TODO: Implement smooth moving of the arrows

            var hourArrowLength = dialFace.Diameter / hourArrowLengthPart;
            var minuteArrowLength = dialFace.Diameter / minuteArrowLengthPart;

            var secondArrowLength = dialFace.Diameter / secondArrowLengthPart;
            var secondArrowCounterweightLength = dialFace.Diameter / secondArrowCounterweightPart;

            double hourArrowAngle = 2 * Math.PI / 12 * (this.currentDateTime.Hour % 12);
            this.DrawSingleArrow(
                canvas,
                dialFace,
                new ArrowInfo(
                    hourArrowLength, 
                    this.hourArrowColor, 
                    hourArrowThickness,
                    hourArrowAngle));

            double minuteArrowAngle = 2 * Math.PI / 60 * this.currentDateTime.Minute;
            this.DrawSingleArrow(
                canvas, 
                dialFace,
                new ArrowInfo(
                    minuteArrowLength, 
                    this.minuteArrowColor, 
                    minuteArrowThickness,
                    minuteArrowAngle));

            double secondArrowAngle = 2 * Math.PI / 60 * this.currentDateTime.Second;
            this.DrawSingleArrow(
                canvas, 
                dialFace,
                new ArrowInfo(
                    secondArrowLength,
                    this.secondArrowColor, 
                    secondArrowThickness,
                    secondArrowAngle,
                    secondArrowCounterweightLength));
        }

        /// <summary>
        /// Draws a bold dot where the arrows connect to each other
        /// </summary>
        /// <param name="canvas">Control canvas</param>
        /// <param name="dialFace">Properties of the dial face</param>
        protected void DrawPointOfAttachment(
            PaintEventArgs canvas,
            DialFaceInfo dialFace)
        {
            var pointDiameter = 
                dialFace.Diameter / pointOfAttachmentDiameterPart;
            var pointBrush = new SolidBrush(this.pointOfAttachmentColor);

            canvas.Graphics.FillEllipse(
                pointBrush,
                dialFace.CenterX - (pointDiameter / 2),
                dialFace.CenterY - (pointDiameter / 2),
                pointDiameter,
                pointDiameter);
        }

        /// <summary>
        /// Draws specific divisions on the dial face
        /// </summary>
        /// <param name="canvas">Control's canvas</param>
        /// <param name="dialFace">Info about the dial face</param>
        /// <param name="divisionCount">Total count of divisions</param>
        /// <param name="divisionLength">Length of the single division</param>
        /// <param name="divisionColor">Color of the single division</param>
        private void DrawDivision(
            PaintEventArgs canvas,
            DialFaceInfo dialFace,
            int divisionCount,
            int divisionLength,
            Color divisionColor)
        {
            var additionToLength = (dialFace.Diameter / 2) - divisionLength;
            var currentAngle = 0.0;
            var divisionPen = new Pen(divisionColor, divisionThickness);

            for (int i = 0; i < divisionCount; ++i)
            {
                var firstPoint = this.ConvertPolarToCartesian(
                    additionToLength,
                    currentAngle,
                    new Point(dialFace.CenterX, dialFace.CenterY));

                var secondPoint = this.ConvertPolarToCartesian(
                    additionToLength + divisionLength,
                    currentAngle,
                    new Point(dialFace.CenterX, dialFace.CenterY));

                canvas.Graphics.DrawLine(divisionPen, firstPoint, secondPoint);

                currentAngle += 2 * Math.PI / divisionCount;
            }
        }

        /// <summary>
        /// Draws single arrow on watches
        /// </summary>
        /// <param name="canvas">Control canvas</param>
        /// <param name="dialFace">Parameters of the dial face</param>
        /// <param name="arrow">Parameters of arrow</param>
        private void DrawSingleArrow(
            PaintEventArgs canvas,
            DialFaceInfo dialFace,
            ArrowInfo arrow)
        {
            // Because arrow.Angle is clockwise diviation from x = 0
            var realAngle = (Math.PI / 2) - arrow.Angle;

            var firstPoint = this.ConvertPolarToCartesian(
                arrow.CounterweightLength,
                realAngle + Math.PI,
                new Point(dialFace.CenterX, dialFace.CenterY));

            var secondPoint = this.ConvertPolarToCartesian(
                arrow.MainPartLength, 
                realAngle,
                new Point(dialFace.CenterX, dialFace.CenterY));

            var arrowPen = new Pen(arrow.ArrowColor, arrow.Thickness);
            canvas.Graphics.DrawLine(arrowPen, firstPoint, secondPoint);
        }

        /// <summary>
        /// Converts polar coordinates to Cartesian coordinates
        /// </summary>
        /// <param name="distance">Distance from origin</param>
        /// <param name="angle">Angle of deflection</param>
        /// <param name="beginPoint">Origin</param>
        /// <returns>Cartesian coordinates</returns>
        private PointF ConvertPolarToCartesian(
            int distance,
            double angle,
            Point beginPoint)
        {
            var cartesianX = beginPoint.X + (distance * Math.Cos(angle));
            var cartesianY = beginPoint.Y - (distance * Math.Sin(angle));

            return new PointF((float)cartesianX, (float)cartesianY);
        }

        /// <summary>
        /// Initializes properties of the control
        /// </summary>
        private void InitializeComponent()
        {
            this.Visible = true;
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Information about the watches' dial face
        /// </summary>
        protected struct DialFaceInfo
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DialFaceInfo"/> struct.
            /// </summary>
            /// <param name="centerX">Circle center abscissa</param>
            /// <param name="centerY">Circle center ordinate</param>
            /// <param name="diameter">Circle diameter</param>
            public DialFaceInfo(int centerX, int centerY, int diameter)
            {
                this.CenterX = centerX;
                this.CenterY = centerY;
                this.Diameter = diameter;
            }

            /// <summary>
            /// Gets or sets x coordinate of the dial face center
            /// </summary>
            public int CenterX { get; set; }

            /// <summary>
            /// Gets or sets y coordinate of the dial face center
            /// </summary>
            public int CenterY { get; set; }

            /// <summary>
            /// Gets or sets the dial face diameter
            /// </summary>
            public int Diameter { get; set; }
        }

        /// <summary>
        /// Parameters of the watch arrow
        /// </summary>
        private struct ArrowInfo
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ArrowInfo"/> struct.
            /// </summary>
            /// <param name="mainLength">Arrow length</param>
            /// <param name="color">Arrow color</param>
            /// <param name="thickness">Arrow thickness</param>
            /// <param name="angle">Angle of clockwise deviation from x = 0</param>
            /// <param name="counterweightLength">Length of the counterweight</param>
            public ArrowInfo(
                int mainLength,
                Color color,
                int thickness,
                double angle,
                int counterweightLength = 0)
            {
                this.MainPartLength = mainLength;
                this.ArrowColor = color;
                this.Thickness = thickness;
                this.Angle = angle;
                this.CounterweightLength = counterweightLength;

            }

            /// <summary>
            /// Gets length of the main part of the arrow
            /// (without counterweight)
            /// </summary>
            public int MainPartLength { get; private set; }

            /// <summary>
            /// Gets length of the counterweight
            /// </summary>
            public int CounterweightLength { get; private set; }

            /// <summary>
            /// Gets arrow color
            /// </summary>
            public Color ArrowColor { get; private set; }

            /// <summary>
            /// Gets arrow thickness
            /// </summary>
            public int Thickness { get; private set; }

            /// <summary>
            /// Gets angle of clockwise deviation from x = 0
            /// </summary>
            public double Angle { get; private set; }
        }
    }
}