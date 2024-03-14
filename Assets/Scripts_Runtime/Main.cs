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

        // 点击 btn_queue 按钮，将消息放入队列，然后在 Update 中发送
        btn_queue.onClick.AddListener(() => {
            SendCore.EnqueuReq1(sendContext, txt1.text, txt2.text);
            SendCore.EnqueueReq2(sendContext, txt3.text, txt4.text);
        });

        // 点击 btn_direct 按钮，直接发送消息
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

        // 每帧发送队列中的消息
        SendCore.TickQueueSend(sendContext, (byte[] bytes) => {
            // 消息写入缓冲区
            ReceiveCore.PreReceiveBuffer(receiveContext, bytes);
        });
        // 每帧接收消息
        ReceiveCore.TickReceiveBuffer(receiveContext);

    }

}