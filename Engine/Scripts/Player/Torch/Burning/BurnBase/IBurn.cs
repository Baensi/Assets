using System;
using UnityEngine;

namespace Engine.Player.Torch.Burn {

    /*
     * IBurn ��������� ��������� ��� ������� ������
     */
    public interface IBurn {

        float getEnergy();
		EBurnType getBurnType();

        void updateBurn();

	}

}

