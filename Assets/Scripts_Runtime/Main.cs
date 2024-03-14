using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MortiseFrame.LitIO;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    [SerializeField] Text txt1;
    [SerializeField] Text txt2;
    [SerializeField] Text txt3;
    [SerializeField] Text txt4;

    [SerializeField] Button btn_queue;
    [SerializeField] Button btn_direct;

    ReceiveContext receiveContext;
    SendContext sendContext;

    void Start() {
        receiveContext = new ReceiveContext();
        sendContext = new SendContext();

        btn_queue.onClick.AddListener(() => {
            SendCore.PreSendReq1WithQueue(sendContext, txt1.text, txt2.text);
            SendCore.PreSendReq2WithQueue(sendContext, txt3.text, txt4.text);
        });

        btn_direct.onClick.AddListener(() => {
            SendCore.DirectSendReq1(sendContext, txt1.text, txt2.text, (byte[] bytes) => {
                ReceiveCore.DirectReadMessage(receiveContext, bytes);
            });
            SendCore.DirectSendReq2(sendContext, txt3.text, txt4.text, (byte[] bytes) => {
                ReceiveCore.DirectReadMessage(receiveContext, bytes);
            });
        });
    }

    void Update() {

        SendCore.TickQueueSend(sendContext, (byte[] bytes) => {
            ReceiveCore.PreReceiveBuffer(receiveContext, bytes);
        });
        ReceiveCore.TickReceiveBuffer(receiveContext);

    }

}