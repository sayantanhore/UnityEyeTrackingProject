using eDriven.Gui;
using eDriven.Gui.Layout;
using eDriven.Gui.Components;
using eDriven.Gui.Containers;
using eDriven.Core.Events;
using UnityEngine;
using System.Collections.Generic;

public class ImageDisplay : eDriven.Gui.Gui {

	public List<ImageBox> images_curr = new List<ImageBox>();
	private int screenHeight;
	public int[] images_to_show = new int[3];
	private Texture texture;
	private GUIStyle labelTextStyle;
	//private int screenWidth;

	override protected void OnInitialize(){
		base.OnInitialize ();
		screenHeight = Screen.height;
		//Layout = new AbsoluteLayout ();
		labelTextStyle = new GUIStyle{
			fontStyle = FontStyle.Italic
		};

		for (int i = 0; i < images_to_show.Length; i ++) {
			images_to_show[i] = Random.Range(0, 20);		
		}
		foreach (int j in images_to_show) {
			Debug.Log("Image No :: " + j);		
		}
		int count = 0;
		foreach (ImageBox imgBox in images_curr) {
			images_curr[count].Texture = (Texture)Resources.Load("eDriven/Editor/Images/im" + (images_to_show[count] + 1));
			count ++;
		}
	}

	private void ImageCreationHandler(eDriven.Core.Events.Event e){
		//print ("Created");
		//print ("Scren height " + screenHeight);
		var width = ((Image)e.Target).Width;
		var height = ((Image)e.Target).Height;

		((Image)e.Target).Height = (float)screenHeight / 4;
		((Image)e.Target).Width = ((Image)e.Target).Height * (float)(width / height);

	}

	override protected void CreateChildren(){
		base.CreateChildren ();
		VGroup vgroup = new VGroup{
			Top = 10,
			Left = 10,
			PercentWidth = 100,
			PercentHeight = 100,
			PaddingBottom = 10,
			HorizontalAlign = HorizontalAlign.Center
		};
		AddChild (vgroup);


		// The current set


		Label lbl_curr = new Label
		{
			PercentHeight = 10,
			HorizontalCenter = 0, // the image is centered (important when not square image)
			VerticalCenter = 0,
			Text = "Current:"
		};

		lbl_curr.SetStyle ("labelstyle", labelTextStyle);

		vgroup.AddChild(lbl_curr);

		var hgroup_curr_img = new HGroup{
			PercentWidth = 100,
			HorizontalCenter = 0,
			HorizontalAlign = HorizontalAlign.Center

		};

		vgroup.AddChild (hgroup_curr_img);
		hgroup_curr_img.AddChild(new Spacer {PercentWidth = 10});
		for (int i = 1; i <= 3; i++)
		{
			ImageBox imgBox = new ImageBox();

			images_curr.Add(imgBox);
			hgroup_curr_img.AddChild(imgBox);
			hgroup_curr_img.AddChild(new Spacer {PercentWidth = 10});

		}
		ModifyImages();

		// The Future Set


		Label lbl_fut = new Label
		{
			//PercentWidth = 30,
			PercentHeight = 10,
			HorizontalCenter = 0, // the image is centered (important when not square image)
			VerticalCenter = 0,
			Text = "Future:"
		};

		vgroup.AddChild (new Spacer {PercentHeight = 10});

		vgroup.AddChild(lbl_fut);
		
		var hgroup_fut_img = new HGroup{
			PercentWidth = 100,
			HorizontalCenter = 0,
			HorizontalAlign = HorizontalAlign.Center
			
		};
		
		vgroup.AddChild (hgroup_fut_img);
		hgroup_fut_img.AddChild(new Spacer {PercentWidth = 10});
		for (int i = 4; i <= 6; i++)
		{
			
			var image = new Image
			{
				Texture = (Texture)Resources.Load("eDriven/Editor/Images/im" + (i + 0)),
				HorizontalCenter = 0, // the image is centered (important when not square image)
				VerticalCenter = 0,
				MouseEnabled = true,
				ScaleMode = ImageScaleMode.ScaleToFit
				
				
			};
			
			image.AddEventListener("creationComplete", ImageCreationHandler);
			//images_curr.Add(image);
			hgroup_fut_img.AddContentChild(image);
			hgroup_fut_img.AddChild(new Spacer {PercentWidth = 10});
		}


	}
	void ModifyImages(){

		foreach (int j in images_to_show) {
			Debug.Log("Image No :: " + j);		
		}


	}
	void Update(){
		//print ("Ok");
	}
}
