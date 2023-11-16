using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;
using System.Threading;

public class AsyncTest : MonoBehaviour
{
	void Start()
	{
		Debug.Log("Run() invoked in Start()");
		Run(10);
		Debug.Log("Run() returns");
	}

	void Update()
	{
		Debug.Log("Update()");
	}

	async void Run(int count)
	{
		// ���� ������� �½�ũ �����忡�� CountAsync()�� �����Ѵ�.
		var task = Task.Run(() => CountAsync(10));

		// �Լ��� �����ϰ� �½�ũ�� ����� ������ ��ٸ���.
		// ���� �ٷ� "Run() returns" �αװ� ��µȴ�.
		// �½�ũ�� ������ result ���� CountAsync() �Լ��� ���ϰ��� ����ȴ�.
		int result = await task;

		// �½�ũ�� ������ await �ٷ� ���� �ٷ� ���ƿͼ� �������� ����ǰ� �Լ��� ����ȴ�.
		Debug.Log("Result : " + result);
	}

	int CountAsync(int count)
	{
		int result = 0;

		for (int i = 0; i < count; ++i)
		{
			Debug.Log(i);
			result += i;
			Thread.Sleep(1000);
		}

		return result;
	}
}
