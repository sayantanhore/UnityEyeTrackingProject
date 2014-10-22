using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Gui.Layout;
using eDriven.Gui.GUIStyles;
using eDriven.Gui.DragDrop;
using Component=eDriven.Gui.Components.Component;
using Event=eDriven.Core.Events.Event;
using UnityEngine;

public class ImageBox : eDriven.Gui.Components.VGroup {

	// Use this for initialization

	private Image image;
	private bool textureChanged;

	private Label lbl;
	private HSlider slider;
	private float feedback;
	public float Feedback{
		get{
			return feedback;
		}
		set{
			if (value == feedback){
				return;
			}
			else{
				feedback = value;
			}
		}
	}
	private Texture texture;
	public Texture Texture{
		get{
			return texture;
		}
		set{
			if (value == texture){
				return;
			}
			texture = value;
			textureChanged = true;
			InvalidateProperties();
		}
	}

	private void SliderDraggedHandler(Event e){
		var slider = e.Target as HSlider;
		float slider_value = (float)(slider.Value / 10.0);
		lbl.Text = slider_value.ToString("0.#");
		Feedback = slider_value;
		Debug.Log (Feedback);
	}
	override protected void CreateChildren(){
		base.CreateChildren ();
		image = new Image
		{
			HorizontalCenter = 0, // the image is centered (important when not square image)
			VerticalCenter = 0,
			MouseEnabled = true,
			ScaleMode = ImageScaleMode.ScaleToFit,
			//Mode = ImageMode.Normal
		};
		
		AddChild (image);

		var hgroup = new HGroup{
			PercentWidth = 100,
			HorizontalCenter = 0,
			HorizontalAlign = HorizontalAlign.Center
		};

		AddChild (hgroup);

		var labelStyles = new System.Collections.Hashtable
		{
			{"labelStyle", eDriven.Gui.GUIStyles.LabelStyle.Instance}
		};

		lbl = new Label{
			HorizontalCenter = 0,
			VerticalCenter = 0,
			Text = "0",
			//Styles = labelStyles
		};

		hgroup.AddChild (lbl);

		slider = new HSlider{
			PercentWidth = 100,
			Height = 30,
			//XMax = 1.0,

			Minimum = 0,
			Maximum = 10,
			Value = 0

		};
		slider.Change += SliderDraggedHandler;
		//slider.AddEventListener (DragEvent.DRAG_DROP, SliderDraggedHandler);
		hgroup.AddChild(slider);

	}

	override protected void CommitProperties(){
		base.CommitProperties ();
		if (textureChanged) {
			textureChanged = false;
			if (null != texture){
				//Debug.Log (texture);
				float width = texture.width;
				float height = texture.height;
				image.Texture = texture;
				//float width = image.Texture.width;
				//float height = image.Texture.height;
				Debug.Log(width + "::" + height);
				image.Height = (float)Screen.height / 3;
				image.Width = image.Height * (float)(width / height);
				this.Height = image.Height;
				this.Width = image.Width;
				this.Visible = true;
			}
		}
	}
}