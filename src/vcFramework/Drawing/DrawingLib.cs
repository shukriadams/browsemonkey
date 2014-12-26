//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com										//
// Compiler requirement : .Net 4.0													//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//																					//
// This program is free software; you can redistribute it and/or modify it under	//
// the terms of the GNU General Public License as published by the Free Software	//
// Foundation; either version 2 of the License, or (at your option) any later		//
// version.																			//
//																					//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY	//
// WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A	//
// PARTICULAR PURPOSE. See the GNU General Public License for more details.			//
//																					//
// You should have received a copy of the GNU General Public License along with		//
// this program; if not, write to the Free Software Foundation, Inc., 59 Temple		//
// Place, Suite 330, Boston, MA 02111-1307 USA										//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace vcFramework.Drawing
{
	/// <summary> Summary description for DrawingServices. </summary>
	public class DrawingLib
	{
		
		/// <summary> Possible positions a point can have outside a rectangle, relative to that rectangle </summary>
		public enum RectanglePositions: int
		{
			Above,
			AboveRight,
			Right,
			BelowRight,
			Below,
			BelowLeft,
			Left,
			AboveLeft
		}


	

		
		/// <summary> Determines if a point lies within the bounds of a rectangle. </summary>
		/// <param name="pntMousePosition"></param>
		/// <param name="rectRectangle"></param>
		/// <returns></returns>
		static public bool PointInRectangle(
			Point pntMousePosition, 
			Rectangle rectRectangle
			)
		{
			if (pntMousePosition.X < rectRectangle.Location.X || pntMousePosition.Y < rectRectangle.Location.Y || pntMousePosition.X > rectRectangle.Location.X + rectRectangle.Size.Width || pntMousePosition.Y > rectRectangle.Location.Y + rectRectangle.Size.Height)
				return false;
			return true;
		}
		

		
		/// <summary> Determines where a point lies outside a rectangle, relative to that rectangle </summary>
		/// <param name="pntMousePosition"></param>
		/// <param name="rectRectangle"></param>
		/// <returns></returns>
		static public RectanglePositions PointPositionOutsideRectangle(
			Point pntMousePosition, 
			Rectangle rectRectangle
			)
		{
			if (pntMousePosition.Y < rectRectangle.Y)
			{
				// mouse lies above rectangle

				if (pntMousePosition.X < rectRectangle.Location.X)
					return RectanglePositions.AboveLeft;
				else if (pntMousePosition.X > rectRectangle.Location.X + rectRectangle.Size.Width)
					return RectanglePositions.AboveRight;
				
				return RectanglePositions.Above;
			}
			else if (pntMousePosition.Y > rectRectangle.Location.Y + rectRectangle.Size.Height)
			{
				// mouse lies below rectangle

				if (pntMousePosition.X < rectRectangle.Location.X)
				{return RectanglePositions.BelowLeft;}
				else if (pntMousePosition.X > rectRectangle.Location.X + rectRectangle.Size.Width)
				{return RectanglePositions.BelowRight;}
				

				return RectanglePositions.Below;
			}
			else
			{
				// mouse is either left or right
				if (pntMousePosition.X < rectRectangle.Location.X)
				{return RectanglePositions.Left;}
				
				// if reaches here, mouse is to right
				return RectanglePositions.Right;
			}
		}



		/// <summary> Returns the pixel width of text for a given font</summary>
		/// <param name="strText"></param>
		/// <param name="fntTextFont"></param>
		/// <returns></returns>
		static public int TextPixelWidth(
			string strText, Font fntTextFont
			)
		{
			if (strText.Length == 0)
				return 0;
			Bitmap objBitmap = null;

			try
			{
				objBitmap = new Bitmap(1,1) ;
				return Graphics.FromImage(objBitmap).MeasureString(strText, fntTextFont).ToSize().Width;
			}
			finally
			{
				objBitmap.Dispose();
				objBitmap = null;
			}
		}

		
		
		/// <summary> Returns the locatin on a control where a given body of text should be drawn. 
		/// Assumes that text fits on control (does not factor over-width into calculations) </summary>
		/// <param name="objControl"></param>
		/// <param name="intPadding"></param>
		/// <param name="strText"></param>
		/// <param name="m_TextAlign"></param>
		/// <returns></returns>
		static public Point ControlTextLocation(
			Control objControl, 
			int intPadding, 
			string strText, 
			System.Drawing.ContentAlignment m_TextAlign
			)
		{
			int intContentsLocationY = 0;
			int intContentsLocationX = 0;

			if (m_TextAlign == System.Drawing.ContentAlignment.TopLeft)
			{
				intContentsLocationX = intPadding;
				intContentsLocationY = intPadding;
			}
			else if (m_TextAlign == System.Drawing.ContentAlignment.MiddleLeft)
			{
				intContentsLocationX = intPadding;
				intContentsLocationY = objControl.Height/2 - objControl.Font.Height/2;
			}
			else if (m_TextAlign == System.Drawing.ContentAlignment.BottomLeft)
			{
				intContentsLocationX = intPadding;
				intContentsLocationY = objControl.Height - intPadding - objControl.Font.Height;
			}
			else if (m_TextAlign == System.Drawing.ContentAlignment.TopCenter)
			{
				intContentsLocationX = objControl.Width/2 - DrawingLib.TextPixelWidth(strText, objControl.Font)/2;
				intContentsLocationY = intPadding;
			}
			else if (m_TextAlign == System.Drawing.ContentAlignment.MiddleCenter)
			{
				intContentsLocationX = objControl.Width/2 - DrawingLib.TextPixelWidth(strText, objControl.Font)/2;
				intContentsLocationY = objControl.Height/2 - objControl.Font.Height/2;
			}
			else if (m_TextAlign == System.Drawing.ContentAlignment.BottomCenter)
			{
				intContentsLocationX = objControl.Width/2 - DrawingLib.TextPixelWidth(strText, objControl.Font)/2;
				intContentsLocationY = objControl.Height - intPadding - objControl.Font.Height;
			}
			else if (m_TextAlign == System.Drawing.ContentAlignment.TopRight)
			{
				intContentsLocationX = objControl.Width - intPadding - DrawingLib.TextPixelWidth(strText, objControl.Font);
				intContentsLocationY = intPadding;
			}
			else if (m_TextAlign == System.Drawing.ContentAlignment.MiddleRight)
			{
				intContentsLocationX = objControl.Width - intPadding - DrawingLib.TextPixelWidth(strText, objControl.Font);
				intContentsLocationY = objControl.Height/2 - objControl.Font.Height/2;
			}
			else if (m_TextAlign == System.Drawing.ContentAlignment.BottomRight)
			{
				intContentsLocationX = objControl.Width - intPadding - DrawingLib.TextPixelWidth(strText, objControl.Font);
				intContentsLocationY = objControl.Height - intPadding - objControl.Font.Height;
			}
	
			Point pntLocation = new Point(intContentsLocationX, intContentsLocationY);
			return pntLocation;
		}
		



		/// <summary> </summary>
		/// <param name="strText"></param>
		/// <param name="fntTextFont"></param>
		/// <param name="intTextPadding"></param>
		/// <param name="intMaxWidth"></param>
		/// <returns></returns>
		static public string TruncateTextToFitPixelWidth(
			string strText, 
			Font fntTextFont, 
			int intTextPadding, 
			int intMaxWidth
			)
		{
			if (strText.Length == 0)
				return "";

			// calculates how much text can be displayed on button
			if (DrawingLib.TextPixelWidth(strText, fntTextFont) + intTextPadding > intMaxWidth )
			{
				while (DrawingLib.TextPixelWidth(strText, fntTextFont) + intTextPadding > intMaxWidth)
				{

					strText = strText.Substring(0, strText.Length - 1);

					// ensures that loop can terminate if text runs out
					if (strText.Length == 0)
						break;
				}
			}

			return strText;
		}



		/// <summary> Draws a bevelled outline around a control </summary>
		/// <param name="objControl"></param>
		/// <param name="g"></param>
		/// <param name="clrBorderColor"></param>
		/// <param name="intBorderThickness"></param>
		static public void DrawBevelledOutlineOnRectangle(
			Rectangle rectDrawRectangle, 
			Graphics g, 
			Color clrBorderColor, 
			int intBorderThickness
			)
		{
			
				Pen objPen = new Pen(clrBorderColor, intBorderThickness);
				Brush solidbrush = new SolidBrush(clrBorderColor);
			
				// when drawing a line thicker than one pixel, it is easier to draw it as  filled rectangle than an actual line
				Rectangle line;

				// need to to get the location of the triangle and factor this into where outline is drawn. if draw to graphics
				// without doing this, everythign will be start off at position (0,0) of the control from which graphics object
				// was taken
				int intRctX = rectDrawRectangle.Location.X;
				int intRctY = rectDrawRectangle.Location.Y;
			
				// aborts method if an invalid border thickness is passed - TODO - should probably throw an exception here instead
				if (intBorderThickness <= 0)
					return;
			
			//	if (rectDrawRectangle.Height == 0 || rectDrawRectangle.Width == 0)
			//		return;

				// border thickness 1 is handled seperately from border thickness > 1 - the former draws proper lines, the latter
				// draws rectangles as lines. not that cannot draw rectangle of pixel width 1 - it always has a minimum dimension of 2
				// because it has a line on all sides
				if (intBorderThickness == 1)
				{
					//top
					g.DrawLine(
						objPen, 
						new Point(intRctX + 1, intRctY + 0), 
						new Point(intRctX + rectDrawRectangle.Width -2, intRctY + 0)
						);

					// bottom
					g.DrawLine(
						objPen, 
						new Point(intRctX + 1 , intRctY + rectDrawRectangle.Height -1 ), 
						new Point(intRctX + rectDrawRectangle.Width - 2, intRctY + rectDrawRectangle.Height - 1)
						);

					// left 
					g.DrawLine(
						objPen, 
						new Point(intRctX + 0 , intRctY + 1 ), 
						new Point(intRctX + 0, intRctY + rectDrawRectangle.Height  - 2)
						);

					// right
					g.DrawLine(
						objPen, 
						new Point(intRctX + rectDrawRectangle.Width -1 , intRctY + 1), 
						new Point(intRctX + rectDrawRectangle.Width - 1, intRctY + rectDrawRectangle.Height - 2)
						);
		


					// top left bevel
					g.DrawLine(
						objPen, 
						new Point(intRctX + 0 , intRctY + 1), 
						new Point(intRctX + 1, intRctY + 1)
						);

					// top right bevel
					g.DrawLine(
						objPen, 
						new Point(intRctX + rectDrawRectangle.Width - 2 , intRctY + 1), 
						new Point(intRctX + rectDrawRectangle.Width, intRctY + 1)
						);

					// bottom left bevel
					g.DrawLine(
						objPen, 
						new Point(intRctX + 0 , intRctY + rectDrawRectangle.Height - 2), 
						new Point(intRctX + 1, intRctY + rectDrawRectangle.Height - 2)
						);

					// bottom right bevel
					g.DrawLine(
						objPen, 
						new Point(intRctX + rectDrawRectangle.Width - 2 , intRctY + rectDrawRectangle.Height - 2), 
						new Point(intRctX + rectDrawRectangle.Width, intRctY + rectDrawRectangle.Height - 2)
						);
			
				}
				else
				{
						
					// top
					line = new Rectangle(
						intRctX + intBorderThickness, 
						intRctY + 0,
						rectDrawRectangle.Width - intBorderThickness*2, 
						intBorderThickness
						);

					g.FillRectangle(solidbrush, line);


					// bottom
					line = new Rectangle(
						intRctX + intBorderThickness, 
						intRctY + rectDrawRectangle.Height-intBorderThickness,									
						rectDrawRectangle.Width - intBorderThickness*2, 
						intBorderThickness
						);

					g.FillRectangle(solidbrush, line);
				
				
					// left
					line = new Rectangle(
						intRctX + 0, 
						intRctY + intBorderThickness,																				
						intBorderThickness, 
						rectDrawRectangle.Height - intBorderThickness*2
						);

					g.FillRectangle(solidbrush, line);
				
				
					// right
					line = new Rectangle(
						intRctX + rectDrawRectangle.Width - intBorderThickness, 
						intRctY + intBorderThickness,									
						intBorderThickness, 
						rectDrawRectangle.Height - intBorderThickness*2
						);

					g.FillRectangle(solidbrush, line);
				
				
					// top left bevel
					line = new Rectangle(
						intRctX + intBorderThickness, 
						intRctY + intBorderThickness,															
						intBorderThickness, 
						intBorderThickness
						);

					g.FillRectangle(solidbrush, line);
				
				
					// top right bevel
					line = new Rectangle(
						intRctX + rectDrawRectangle.Width - intBorderThickness*2, 
						intRctY + intBorderThickness,								
						intBorderThickness, 
						intBorderThickness
						);

					g.FillRectangle(solidbrush, line);
				
				
					// bottom left bevel
					line = new Rectangle(
						intRctX + intBorderThickness, 
						intRctY + rectDrawRectangle.Height - intBorderThickness*2,								
						intBorderThickness, 
						intBorderThickness
						);

					g.FillRectangle(solidbrush, line);
				
				
					// bottom right bevel
					line = new Rectangle(
						intRctX + rectDrawRectangle.Width - intBorderThickness*2, 
						intRctY + rectDrawRectangle.Height - intBorderThickness*2,	
						intBorderThickness, 
						intBorderThickness
						);

					g.FillRectangle(solidbrush, line);


				}
				objPen.Dispose();
				solidbrush.Dispose();

		}




		/// <summary> Draws a square outline around a control </summary>
		/// <param name="objControl"></param>
		/// <param name="g"></param>
		/// <param name="clrBorderColor"></param>
		/// <param name="intBorderThickness"></param>
		static public void DrawBoxOutlineOnRectangle(
			Rectangle rctDrawRectangle, 
			Graphics g, 
			Color clrBorderColor, 
			int intBorderThickness,
			int intPushIn
			)
		{
			
			if (intBorderThickness <= 0)
				return;

		//	if (rctDrawRectangle.Height == 0 || rctDrawRectangle.Width == 0)
		//		return;


			Pen objPen = new Pen(clrBorderColor, intBorderThickness);
			Brush solidbrush = new SolidBrush(clrBorderColor);
			Rectangle line;
			int WidthCorrector = 0;

			// need to correct for width when not pushing in, ie, when drawing borders right up against edge of control
			// borders at bottom and right need to pushed out by 1 if drawing there. when not drawing right at edge,
			// do not need to corrects
			if (intPushIn == 0)
				WidthCorrector = 1;

			if (intBorderThickness == 1)
			{
				//top
				g.DrawLine(objPen, new Point(0 + intPushIn, 0 + intPushIn),										new Point(rctDrawRectangle.Width - intPushIn, 0 + intPushIn));
				// bottom
				g.DrawLine(objPen, new Point(0 + intPushIn, rctDrawRectangle.Height - WidthCorrector - intPushIn),	new Point(rctDrawRectangle.Width - intPushIn, rctDrawRectangle.Height - WidthCorrector - intPushIn));
				// left 
				g.DrawLine(objPen, new Point(0 + intPushIn, 0 + intPushIn),										new Point(0 + intPushIn, rctDrawRectangle.Height - intPushIn));
				// right
				g.DrawLine(objPen, new Point(rctDrawRectangle.Width - WidthCorrector - intPushIn, 0 + intPushIn),		new Point(rctDrawRectangle.Width - WidthCorrector - intPushIn, rctDrawRectangle.Height - intPushIn));

			}
			else
			{
										
				// top
				line = new Rectangle(0 + intPushIn, 0 + intPushIn,											rctDrawRectangle.Width - intPushIn*2, intBorderThickness);
				g.FillRectangle(solidbrush, line);
				
				// bottom
				line = new Rectangle(0 + intPushIn, rctDrawRectangle.Height - intBorderThickness - intPushIn,		rctDrawRectangle.Width - intPushIn*2, intBorderThickness);
				g.FillRectangle(solidbrush, line);
				
				// left
				line = new Rectangle(0 + intPushIn, 0 + intPushIn,											intBorderThickness, rctDrawRectangle.Height - intPushIn*2);
				g.FillRectangle(solidbrush, line);
				
				// right
				line = new Rectangle(rctDrawRectangle.Width - intBorderThickness - intPushIn, 0 + intPushIn,		intBorderThickness, rctDrawRectangle.Height - intPushIn*2);
				g.FillRectangle(solidbrush, line);
			}

			objPen.Dispose();
			solidbrush.Dispose();
		}

		
		
		/// <summary> Returns a font for the given Xmlnode of style info </summary>
		/// <param name="nXmlStyleData"></param>
		/// <returns></returns>
		static public Font FontFromStyleXml(
			XmlNode nXmlStyleData
			)
		{
			bool blnApplyFontStyle = false;
			Font returnFont = null;

			if (nXmlStyleData.SelectSingleNode(".//fontStyle") != null)
				blnApplyFontStyle = true;
			

			if (!blnApplyFontStyle)
			{
				// applies font and font size
				returnFont = new Font(
					nXmlStyleData.SelectSingleNode(".//fontFamily").InnerText,
					Convert.ToSingle(nXmlStyleData.SelectSingleNode(".//fontSize").InnerText)
					);
			}
			else
			{

				// default - applies font, font size and font style
				returnFont = new Font(
					nXmlStyleData.SelectSingleNode(".//fontFamily").InnerText,
					Convert.ToSingle(nXmlStyleData.SelectSingleNode(".//fontSize").InnerText),
					(FontStyle)Enum.Parse(typeof(FontStyle), nXmlStyleData.SelectSingleNode(".//fontStyle").InnerText)
					);

			}

			return returnFont;

		}


	}
}
