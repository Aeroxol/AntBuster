using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanonEmplace : MonoBehaviour
{
    private Button emplaceBtn;

    private void Start()
    {
        emplaceBtn = GetComponent<Button>();
        emplaceBtn.onClick.AddListener(BtnCallBack);
    }

    private void BtnCallBack()
    {
        print("버튼 작동 테스트");
    }
}
