using UnityEngine;

namespace FreeDraw
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Collider2D))]
	public class Drawable : MonoBehaviour
	{
		public delegate void Brush_Function(Vector2 world_position);

		public static Color Pen_Colour;

		public static int Pen_Width;

		public Brush_Function current_brush;

		public LayerMask Drawing_Layers;

		public bool Reset_Canvas_On_Play;

		public Color Reset_Colour;

		public static Drawable drawable;

		private Sprite drawable_sprite;

		private Texture2D drawable_texture;

		private Vector2 previous_drag_position;

		private Color[] clean_colours_array;

		private Color transparent;

		private Color32[] cur_colors;

		private bool mouse_was_previously_held_down;

		private bool no_drawing_on_current_drag;

		public void BrushTemplate(Vector2 world_position)
		{
		}

		public void PenBrush(Vector2 world_point)
		{
		}

		public void SetPenBrush()
		{
		}

		private void Update()
		{
		}

		public void ColourBetween(Vector2 start_point, Vector2 end_point, int width, Color color)
		{
		}

		public void MarkPixelsToColour(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
		{
		}

		public void MarkPixelToChange(int x, int y, Color color)
		{
		}

		public void ApplyMarkedPixelChanges()
		{
		}

		public void ColourPixels(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
		{
		}

		public Vector2 WorldToPixelCoordinates(Vector2 world_position)
		{
			return default(Vector2);
		}

		public void ResetCanvas()
		{
		}

		private void Awake()
		{
		}
	}
}
