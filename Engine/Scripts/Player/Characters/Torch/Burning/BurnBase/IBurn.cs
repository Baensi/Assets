using System;
using UnityEngine;

namespace Engine.Player.Torch.Burn {

    /*
     * IBurn описывает интерфейс для топлива факела
     */
    public interface IBurn {

        float getEnergy();
		EBurnType getBurnType();

        void updateBurn();

	}

}

