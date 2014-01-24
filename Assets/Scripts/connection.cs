using UnityEngine;
using System;
using System.Collections;
using System.Collections.Concurrent;
using SocketIOClient;


public class connection : MonoBehaviour {
	private Client socket;

	// Use this for initialization
	void Start() {
		try
		{
			string socketUrl = "http://10.85.18.93:8090";
			Debug.Log("socket url: " + socketUrl);
			
			
			this.socket = new Client(socketUrl);
			//this.socket.Opened += this.SocketOpened;
			//this.socket.Message += this.SocketMessage;
			//this.socket.SocketConnectionClosed += this.SocketConnectionClosed;
			//this.socket.Error += this.SocketError;
			
			this.socket.On("pukka", (data) => {
				Debug.Log(data.Json.args[0]);
			});
			
			this.socket.Connect();
		}
		catch(Exception e)
		{
			Debug.Log("Connection failed.");
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnApplicationQuit () {
		this.socket.Close();
	}
}
