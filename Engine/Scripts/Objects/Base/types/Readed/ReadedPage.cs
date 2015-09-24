using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Engine.Objects.Types;

namespace Engine.Objects.Types.Readed {

	public class ReadedPage {

		private Texture texture;
		private float   readDelay = 0;
		private bool    readed    = false;
	
		public ReadedPage(Texture texture, int readDelay){
			this.texture=texture;
			this.readDelay=readDelay;
		}

		public Texture getPage(){
			return texture;
		}
		
		public void setPage(Texture texture){
			this.texture=texture;
		}
		
		public float getReadDelay(){
			return readDelay;
		}
		
		public void setReadDelay(float readDelay){
			this.readDelay=readDelay;
		}
		
		public bool isReaded {
			get {return readed;}
			set {readed = value;}
		}
		
	}
	
}

