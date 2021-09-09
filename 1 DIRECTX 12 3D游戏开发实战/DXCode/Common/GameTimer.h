//***************************************************************************************
// GameTimer.h by Frank Luna (C) 2011 All Rights Reserved.
//***************************************************************************************

#ifndef GAMETIMER_H
#define GAMETIMER_H

class GameTimer
{
public:
	GameTimer();
	/// <summary>
	/// ������ʱ��
	/// </summary>
	/// <returns></returns>
	float TotalTime()const;
	/// <summary>
	/// ����Tick�������
	/// </summary>
	/// <returns></returns>
	float DeltaTime()const; 
	/// <summary>
	/// Reset����ʼʱ�����һ��
	/// </summary>
	void Reset(); 
	/// <summary>
	/// ��ʼ��ֹ֮ͣ��Ҳ�����ٿ�ʼ
	/// </summary>
	void Start(); 
	/// <summary>
	/// ֹͣ��Stop
	/// </summary>
	void Stop(); 
	/// <summary>
	/// ÿһ֡��tick
	/// </summary>
	void Tick();  

private:
	/// <summary>
	/// ÿ����ٴ�Count
	/// </summary>
	double mSecondsPerCount;
	/// <summary>
	/// ����Tick�������
	/// </summary>
	double mDeltaTime;
	/// <summary>
	/// Reset��ʱ�̼���
	/// </summary>
	__int64 mBaseTime;
	/// <summary>
	/// ��ͣ��ʱ��
	/// </summary>
	__int64 mPausedTime;

	__int64 mStopTime;
	/// <summary>
	/// ��һ��tick��ʱ�̼���
	/// </summary>
	__int64 mPrevTime;
	/// <summary>
	/// ��һ��tick��ʱ�̼���
	/// </summary>
	__int64 mCurrTime;
	/// <summary>
	/// �Ƿ�ֹͣ
	/// </summary>
	bool mStopped;
};

#endif // GAMETIMER_H