using System;
using UnityEngine;

namespace Engine.Player.Torch.Burn {

	public interface IBurnRender {

		void setBurn(IBurn burn);

		void drawBurn();
		void setupLight();
	
		void update();

	}

}