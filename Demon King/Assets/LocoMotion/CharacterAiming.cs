using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAiming : MonoBehaviour
{

    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (!PV.IsMine)
            return;
    }
}
