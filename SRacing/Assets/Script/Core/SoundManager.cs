using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable] // Ŭ���� ���� ����(����)���� �ν����Ϳ� ǥ�� �� �� �ֵ���
public class Sound  // ������Ʈ �߰� �Ұ���.  MonoBehaviour ��� �� �޾Ƽ�. �׳� C# Ŭ����.
{
	public string Name;  // �� �̸�
	public AudioClip Clip;  // ��
}

public class SoundManager : BaseManager
{
	static public SoundManager instance;  // �ڱ� �ڽ��� ���� �ڿ�����. static�� ���� �ٲ� �����ȴ�.

	[SerializeField]
	private Sound[] SFXSounds;  // ȿ���� ����� Ŭ����

	[SerializeField]
	private Sound[] BgmSounds;  // BGM ����� Ŭ����

	[SerializeField]
	private AudioSource AudioSourceBgm;  // BGM �����. BGM ����� �� �������� �̷������ �ǹǷ� �迭 X 

	[SerializeField]
	private AudioSource[] AudioSourceSFX;  // ȿ�������� ���ÿ� �������� ����� �� �����Ƿ� 'mp3 �����'�� �迭�� ����

	private void Awake()  // ��ü ������ ���� ���� (�׷��� �̱����� ���⼭ ����)
	{
		if (instance == null)  // �� �ϳ��� �����ϰԲ�
		{
			instance = this;  // ��ü ������ instance�� �ڱ� �ڽ��� �־���
							  //DontDestroyOnLoad(gameObject);  // �� �ٲ� �� �ڱ� �ڽ� �ı� ����
		}
		else
			Destroy(this.gameObject);
	}

	public void PlaySFX(string _name)
	{
		// If not "Crash," play the other SFX sounds
		for (int j = 0; j < AudioSourceSFX.Length; j++)
		{
			if (false == AudioSourceSFX[j].isPlaying)
			{
				for (int i = 0; i < SFXSounds.Length; i++)
				{
					if (_name == SFXSounds[i].Name)
					{
						AudioSourceSFX[j].clip = SFXSounds[i].Clip;
						AudioSourceSFX[j].Play();
						return;
					}
				}
				Debug.Log(_name + " ���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
				return;
			}
		}
		Debug.Log("��� ���� AudioSource�� ��� ���Դϴ�.");
	}



	public void PlayBGM(string _name)
	{
		for (int i = 0; i < BgmSounds.Length; i++)
		{
			if (_name == BgmSounds[i].Name)
			{
				AudioSourceBgm.clip = BgmSounds[i].Clip;
				AudioSourceBgm.Play();
				return;
			}
		}
		Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
	}

	public void StopSFX(string _name)
	{
		for (int i = 0; i < AudioSourceSFX.Length; i++)
		{
			if (AudioSourceSFX[i].isPlaying && AudioSourceSFX[i].clip != null && _name == GetSoundNameByClip(AudioSourceSFX[i].clip))
			{
				AudioSourceSFX[i].Stop();
				return;
			}
		}

		Debug.Log(_name + " ���尡 ���� ��� ������ �ʰų� SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
	}

	// Example method to retrieve sound name by AudioClip (replace with your actual implementation)
	private string GetSoundNameByClip(AudioClip clip)
	{
		for (int i = 0; i < SFXSounds.Length; i++)
		{
			if (SFXSounds[i].Clip == clip)
			{
				return SFXSounds[i].Name;
			}
		}

		return null;
	}

	public void StopAllSFX()
	{
		for (int i = 0; i < AudioSourceSFX.Length; i++)
		{
			AudioSourceSFX[i].Stop();
		}
	}
}