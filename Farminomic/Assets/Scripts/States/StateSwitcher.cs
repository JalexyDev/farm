using System;
using System.Collections.Generic;
using UnityEngine;

public class StateSwitcher : MonoBehaviour, ITimeReceiver
{
    public List<TimeState> States;

    private TimeController timeController;
    private TimeState currentState;
    private DateTime nextStateDate;

    // экшн выполняемый при переходе с одной стадии на другую (например смена спрайта, замена награды в этой стадии и т.п.)
    private Action<TimeState> betweenStatesAction;
    // экшн на последней стадии. Тут можно переопределить награду, или удалить этот компонент (напрмер у здания после постройки)
    private Action<TimeState> lastStateAction;
    // экшн для растений который выполнится когда завершится последняя последняя стадия (например удалится с грядки или вернется к состоянию без плодов)
    private Action<TimeState> finishAction;

    private bool hasNextState;

    private void OnDestroy()
    {
        GetTimeController().Unsubscribe(this);
    }

    public void StartSwitching()
    {
        if (currentState == null)
        {
            hasNextState = true;
            SwitchState(States[0]);
            betweenStatesAction(currentState);
        }

        GetTimeController().SubscribeOnTimeUpdates(this);
    }

    public void SetBetweenStatesAction(Action<TimeState> action)
    {
        betweenStatesAction = action;
    }

    public void SetLastStateAction(Action<TimeState> action)
    {
        lastStateAction = action;
    }

    public void SetFinishAction(Action<TimeState> action)
    {
        finishAction = action;
    }

    public void AddTimeState(TimeState state)
    {
        States.Add(state);
        hasNextState = true;
        GetTimeController().SubscribeOnTimeUpdates(this);
    }

    public void SelectState(int stateNumber)
    {
        hasNextState = true;
        SwitchState(States[stateNumber]);
        betweenStatesAction(currentState);
    }

    public void ReceiveTime(DateTime currentTime)
    {
        if (hasNextState)
        {
            if (currentTime >= nextStateDate)
            {
                int index = States.IndexOf(currentState) + 1;

                if (index >= States.Count && finishAction != null)
                {
                    hasNextState = false;
                    // выполняем действия, которые нужно выполнить после последней стадии (например удалить этот компонент)
                    finishAction(currentState);

                    return;
                }

                SwitchState(States[index]);

                if (index < States.Count - 1 && betweenStatesAction != null)
                {
                    // выполняем действия, которые нужно выполнить при переходе с одной стадии на другую
                    betweenStatesAction(currentState);
                }
                else if (index == States.Count - 1 && lastStateAction != null)
                {
                    // выполняем действие, которое выполняется на последней стадии (переопределить награду у растений, например)
                    lastStateAction(currentState);
                }

                // если игрок выходил и пропустил несколько стадий, то с помощью рекурсии пробегаемся по всем пройденным стадиям.
                ReceiveTime(currentTime);
            }   
        }
        else
        {
            GetTimeController().Unsubscribe(this);
        }
    }

    private void SwitchState(TimeState state)
    {
        currentState = state;

        //todo заменить на AddMinutes

        if (nextStateDate == DateTime.MinValue)
        {
            nextStateDate = GetTimeController().GetCurrentTime().AddSeconds(currentState.DurationMinutes);
        }
        else
        {
            nextStateDate = nextStateDate.AddSeconds(currentState.DurationMinutes);
        }
    }

    private TimeController GetTimeController()
    {
        if (timeController == null)
        {
            timeController = TimeController.Instance;
        }

        return timeController;
    }

    public override bool Equals(object obj)
    {
        return obj is StateSwitcher switcher &&
               base.Equals(obj) &&
               name == switcher.name &&
               EqualityComparer<GameObject>.Default.Equals(gameObject, switcher.gameObject);
    }

    public override int GetHashCode()
    {
        int hashCode = -1793005472;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
        hashCode = hashCode * -1521134295 + EqualityComparer<GameObject>.Default.GetHashCode(gameObject);
        return hashCode;
    }
}
