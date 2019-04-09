// GameScript for Open Source Game by Android Helper Games
// v.1.0
// Если увидите ошибки, то поймите меня, я писал быстро)
// Скрипт на 5 % оптимизирован, так что могут быть баги и т.д.

using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Используем библиотеку UnityEngine.UI для управления интерфейсом (Текст, изображения и т.д).
using System.Collections.Generic;
using System.Runtime;

class Question
{
    public int quest;
    public int[] falseAnsver = new int[3];
    public int trueAnswer;
}


public class GameScript : MonoBehaviour
{
    private Question[] quests = new Question[10];
    private bool[] checkDict = new bool[40];
    private string[,] dict = new string[40, 2];

    private int[] bufQ = new int[10];
    private List<int> startBufQ = new List<int>();
    private int rAns = 0;

    public Text timeText; // Текст для отображения времени.
    public Text quastion; // Текст для отображения вопросов.
    public Text[] answers; //Массив | Текст для отображения вариантов ответов.
    public Text[] Stats; //Массив | Текст для отображения статистики при ответе.
    public Text[] debugText; //Массив | Текст для отображения дебага.
    public Button[] Bttns; //Массив | Кнопки ответов.
    public Sprite trueAnswer; //Спрайт при правильном ответе.
    public Sprite falseAnswer; //Спрайт при неправильном ответе.
    public Sprite clickedAnswerS; // //Спрайт при нажатии на кнопку.
    public Image fadeImage;     //Картинка для затухания при загрузке.
    public Image[] bttnsImages; //Массив | Тут хранятся картинки кнопок.
    public Animator statsAnimation; // Анимация при появлении статистики.
    public GameObject pausePanel; // Панель паузы.
    public int wQ = 1; // Число с нумерацией вопроса.
    private int clickedAnswer = -1; // Число (Можно так сказать индекс) нажатого варианта ответа.
    private int money = 0; // Число с балансом денег.
    private int trueint = -1; // Число с правильным ответом.
    private int debugTggl = 0; // Число с настройкой (Включен ли дебаг [1 - да, 0 - нет]).
    private int timeEq = 10; // Число отсчёта времени.
    private float fadeTime = 4.5f; // Скорость перехода при загрузке.
    private float timeF = 0; // Значение для расчёта времени.
    private bool getMoney = false; // Равен true если деньги уже были даны.
    private bool lose = false; // Если ответ неверный, то равно true.
    private bool goFadeIn = false; // Надо ли делать переход при загрузке.
    private bool goFadeOut = false; // Надо ли делать переход (из черноты) при загруженом уровне.
    private bool dontlikeit = false; // Игра не понравилась? # ТАГДА УХАДИ
    private bool ended = false; // Закончена ли игра полностью?
    private bool pauseActive = false; // Если включена пауза, то равно true.
    private bool isLoaded = false; // Если уровень загружен, то равно true.
    private bool trueA = false; // Равно true если ответ верный.
    private bool falseA = false; // Равно true если ответ неверный.

    private void initDict()
    {
        dict[0, 0] = "Гәүдә";
        dict[0, 1] = "Тело";

        dict[1, 0] = "Баш";
        dict[1, 1] = "Голова";

        dict[2, 0] = "Чәч";
        dict[2, 1] = "Волосы";

        dict[3, 0] = "Йөз";
        dict[3, 1] = "Лицо";

        dict[3, 0] = "Маңгай";
        dict[3, 1] = "Лоб";

        dict[4, 0] = "Каш";
        dict[4, 1] = "Бровь";

        dict[5, 0] = "Керфек";
        dict[5, 1] = "Ресница";

        dict[5, 0] = "Күз";
        dict[5, 1] = "Глаз";

        dict[6, 0] = "Борын";
        dict[6, 1] = "Нос";

        dict[6, 0] = "Ирен";
        dict[6, 1] = "Губы";

        dict[7, 0] = "Авыз";
        dict[7, 1] = "Рот";

        dict[8, 0] = "Кул";
        dict[8, 1] = "Рука";

        dict[9, 0] = "Бармак";
        dict[9, 1] = "Палец";

        dict[10, 0] = "Өрек ";
        dict[10, 1] = "Абрикос";

        dict[11, 0] = "Әфлисун ";
        dict[11, 1] = "Апельсин";

        dict[12, 0] = " Йөзем ";
        dict[12, 1] = "Виноград";

        dict[13, 0] = "Керфек";
        dict[13, 1] = "Ресница";

        dict[14, 0] = "Анар";
        dict[14, 1] = "Гранат";

        dict[15, 0] = "Армут";
        dict[15, 1] = "Груша";

        dict[16, 0] = "Кавын";
        dict[16, 1] = "Дыня";

        dict[17, 0] = "Шәфталу";
        dict[17, 1] = "Персик";

        dict[18, 0] = "Караҗимеш";
        dict[18, 1] = "Слива";

        dict[19, 0] = "Хөрмә ";
        dict[19, 1] = "Хурма";

        dict[20, 0] = "Алма ";
        dict[20, 1] = "Яблоко";

        dict[21, 0] = " пәлтә";
        dict[21, 1] = "Пальто";

        dict[21, 0] = " итек";
        dict[21, 1] = "Сапоги";

        dict[22, 0] = " Кроссовкалар";
        dict[22, 1] = "Кросовки";

        dict[23, 0] = "Оекбаш";
        dict[23, 1] = "Носки";

        dict[24, 0] = "эшләпә";
        dict[24, 1] = "Шляпа";

        dict[25, 0] = "чалбар";
        dict[25, 1] = "Брюки";

        dict[26, 0] = "  күлмәк";
        dict[26, 1] = "Рубашка";

        dict[27, 0] = "Безрукавка";
        dict[27, 1] = "Безрукавка";

        dict[28, 0] = "түбәтәй";
        dict[28, 1] = "Тубитиейка";

        dict[29, 0] = "Кызлыр";
        dict[29, 1] = "Красный";

        dict[30, 0] = "Кызгылт сары";
        dict[30, 1] = "оранжевый";

        dict[31, 0] = "Ал";
        dict[31, 1] = "Розовый";

        dict[32, 0] = "Яшел";
        dict[32, 1] = "Зелёный";

        dict[33, 0] = "Ак ";
        dict[33, 1] = "Белый";

        dict[34, 0] = " Зәңгәр ";
        dict[34, 1] = "Синий";

        dict[35, 0] = "Күк  ";
        dict[35, 1] = "Голубой";

        dict[36, 0] = " Сары ";
        dict[36, 1] = "Жёлтый";

        dict[37, 0] = " Соры ";
        dict[37, 1] = "Серый";

        dict[38, 0] = "Көрән ";
        dict[38, 1] = "коричневый";

        dict[39, 0] = " Шәмәхә ";
        dict[39, 1] = "Фиолетовый";
    }

    private void InitQuests()
    {
        for (int i = 0; i < 10; ++i)
            quests[i] = new Question();

        for (int i = 0; i < 40; ++i)
            checkDict[i] = true;

        int max = 40;
        for (int n = 0; n < 10; ++n)
        {
            int ind = 0;
            int select = Random.Range(0, max);
            quests[n].trueAnswer = Random.Range(0, 4);
            --max;
            while (true)
            {
                if (checkDict[ind] && select == 0)
                    break;
                else if (checkDict[ind])
                    select--;
                ++ind;
            }
            quests[n].quest = ind;
            checkDict[ind] = false;
        }

        max = 39;
        for (int n = 0; n < 10; ++n)
        {
            for (int i = 0; i < 40; ++i)
                checkDict[i] = true;
            checkDict[quests[n].quest] = false;

            for (int a = 0; a < 3; ++a)
            {
                int ind = 0;
                int select = Random.Range(0, max);
                --max;
                while (true)
                {
                    if (checkDict[ind] && select == 0)
                        break;
                    else if (checkDict[ind])
                        select--;
                    ++ind;
                }
                quests[n].falseAnsver[a] = ind;
                checkDict[ind] = false;
            }
        }
    }

    void Awake() // Данная функция вызывается до инициализации всех остальных скриптов. Обычно используется для установки определенных параметров и инициализации переменных.
    {
        initDict();
        InitQuests();

        fadeImage.enabled = true; // Включить чёрный фон для перехода.
        goFadeOut = true; //Делаем переход (из черноты) при загруженом уровне.

        if (PlayerPrefs.HasKey("M") && PlayerPrefs.HasKey("wQ")) // Если в сохранённых данных есть эти значения то:
        {
            rAns = PlayerPrefs.GetInt("M"); // Взять переменную из сохранений.
            wQ = PlayerPrefs.GetInt("wQ"); // Взять переменную из сохранений.
        }
        else // Если данных нет, то:
        {
            PlayerPrefs.SetInt("wQ", 1); // Сохранить переменную.
            PlayerPrefs.SetInt("M", 0); // Сохранить переменную.
        }
        debugTggl = PlayerPrefs.GetInt("DebugMode"); // Взять переменную (включен ли дебаг мод) из сохранений.
        PlayerPrefs.Save(); // Сохранить изменения в сохранениях :)
    }
    void Update() // Данная функция вызывается каждый раз перед отображением очередного кадра. Самая используемая для расчетов игровых параметров.
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Если нажата кнопка (Escape), либо кнопка назад на телефоне, то:
        {
            if (pauseActive == false) // Если пауза не активна, то:
            {
                pausePanel.SetActive(true); // Включить панель паузы.
                pauseActive = true; // Если включена пауза, то равно true.
            }
            else // Если пауза активна, то:
            {
                pausePanel.SetActive(false); // Выключить панель паузы.
                pauseActive = false; //Если выключена пауза, то равно false.
            }
        }
        if (debugTggl == 1)// Если значение debugTggl равно 1, то:
        {
            debugText[0].enabled = true; // Включить текст дебага.
            debugText[0].text = "Дебаг мод: Число с нумерацией вопроса = " + wQ.ToString(); // Присвоить текст.
            debugText[1].enabled = true; // Включить текст дебага.
            debugText[1].text = "Кликнутый ответ = " + clickedAnswer.ToString(); // Присвоить текст.
            debugText[2].enabled = true; // Включить текст дебага.
            debugText[2].text = "Правильный ответ = " + trueint.ToString(); // Присвоить текст.
        }

        timeText.text = timeEq.ToString(); // Присвоить тексту число переведённое в строку.
        timeF += Time.deltaTime; // Складываем значение для расчёта времени.
        if (timeF >= 1.2) // Если значение больше либо равно 1.2, то:
        {
            if (clickedAnswer == -1) // Если не была нажата кнопка ответа, то:
            {
                if (pauseActive == false) // Если не была включена пауза, то:
                {
                    timeEq--; // Уменьшаем число времени на 1.
                    timeF = 0; // Значение для расчёта времени равно 0;
                }
            }
        }
        if (timeEq <= 0) // Если число отсчёта времени меньше либо равно 0, то:
        {
            timeText.text = "Время истекло"; // Присвоить тексту строку.
            overTime(); // Запустить функцию (overTime).
        }
        if (timeEq <= 3)
        {
            timeText.color = Color.red;
        }
        
        RQ(wQ-1);
 
    }

    void FixedUpdate() // Данная функция вызывается каждый раз при расчете физических показателей. Все расчеты физики следует проводить именно в ней. Но я рассчитываю анимацию перехода, и использую эту функцию для того, чтобы не было никаких тормозов.
    {
        if (goFadeIn == true) // Если [Надо ли делать переход при загрузке.] = true, то:
        {
            fadeImage.color = Color.Lerp(fadeImage.color, Color.black, fadeTime * Time.deltaTime); // Делаем переход из цвета картинки в чёрный.
        }
        else if (goFadeOut == true) // Иначе если [Надо ли делать переход (из черноты) при загруженом уровне.] = true, то:
        {
            fadeImage.color = Color.Lerp(fadeImage.color, Color.clear, fadeTime * Time.deltaTime); // Делаем переход из цвета картинки (чёрный) в прозрачный.
        }
        if (fadeImage.color == Color.black) // Если цвет картинки = чёрному, то:
        {
            if (isLoaded == false) // Если уровень не загружен, то:
            {
                continueLoad(); // Запускаем функцию continueLoad (416 строка).
            }
        }

    }

    private void RQ(int n)
    {
        quastion.text = dict[quests[n].quest, 0]; // Задаём вопрос.

        trueint = quests[n].trueAnswer; // Правильный ответ

        for (int i = 0, a = 0; i < 4; ++i)
        {
            if (i == trueint)
            {
                answers[i].text = dict[quests[n].quest, 1];
            }
            else
            {
                answers[i].text = dict[quests[n].falseAnsver[a], 1];
                ++a;
            }
        }

        Stats[1].text = " "; // Текст при показании статистики.
        if (n == 9)
            ended = true;

        if (clickedAnswer == trueint) // Если нажатый ответ равен 1, то:
        {
            StartCoroutine(waitForMagic()); // Запускаем корутину выигрыша (Корутина - простой и удобный способ запускать функции, которые должны работать параллельно в течение некоторого времени).
        }
        else if (clickedAnswer != -1) // Иначе если нажатый ответ равен 0 или 2 или 3, то:
        {
            StartCoroutine(waitForDestroy()); // Запускаем корутину проигрыша.
        }


    }

    IEnumerator waitForMagic() // Корутина выигрыша.
    {
        Bttns[0].interactable = false; // Отключить нажатие кнопки 1.
        Bttns[1].interactable = false; // Отключить нажатие кнопки 2.
        Bttns[2].interactable = false; // Отключить нажатие кнопки 3.
        Bttns[3].interactable = false; // Отключить нажатие кнопки 4.
        trueA = true; // Если ответ правильный, то равно true.
        bttnsImages[clickedAnswer].sprite = clickedAnswerS; // Присвоить кнопке спрайт нажатой кнопки.
        yield return new WaitForSeconds(3f); // Продолжить через 3 сек.
        bttnsImages[clickedAnswer].overrideSprite = trueAnswer; // Присвоить кнопке спрайт правильно нажатой кнопки.
        yield return new WaitForSeconds(2f); // Продолжить через 2 сек.
        StatsWindow(); // Запустить функцию статистики.
        StopCoroutine(waitForMagic()); // Остановить корутину выигрыша.
    }
    IEnumerator waitForDestroy() // Корутина проигрыша.
    {
        Bttns[0].interactable = false; // Отключить нажатие кнопки 1.
        Bttns[1].interactable = false; // Отключить нажатие кнопки 2.
        Bttns[2].interactable = false; // Отключить нажатие кнопки 3.
        Bttns[3].interactable = false; // Отключить нажатие кнопки 4.
        bttnsImages[clickedAnswer].sprite = clickedAnswerS; // Присвоить кнопке спрайт нажатой кнопки.
        falseA = true; // Если ответ неправильный, то равно true.
        yield return new WaitForSeconds(3f); // Продолжить через 3 сек.
        bttnsImages[clickedAnswer].overrideSprite = falseAnswer; // Присвоить кнопке спрайт неправильно нажатой кнопки.
        bttnsImages[trueint].sprite = trueAnswer; // Присвоить спрайт кнопке, которая была правильным ответом.
                                                  /*if (dontlikeit == true) // Если dontlikeit = true, то:
                                                  {
                                                      Application.Quit(); // Выйти из игры. Возможна ошибка при выходе.
                                                  }
                                                  else // Иначе:
                                                  {*/
        yield return new WaitForSeconds(3f); // Продолжить через 3 сек.
        StatsWindow(); // Запустить функцию статистики.
        StopCoroutine(waitForDestroy()); // Остановить корутину проигрыша.
        //}
    }
    private void overTime() // Приватная функция (overTime)
    {
        Bttns[0].interactable = false; // Отключить нажатие кнопки 1.
        Bttns[1].interactable = false; // Отключить нажатие кнопки 2.
        Bttns[2].interactable = false; // Отключить нажатие кнопки 3.
        Bttns[3].interactable = false; // Отключить нажатие кнопки 4.
        StatsWindow(); // Запустить функцию (Статистика)
    }
    private void StatsWindow() // Приватная функция (Окно статистики)
    {
        if (trueA == true) // Если нажат правильный ответ, то:
        {
            if (getMoney == false) // Если деньги не давались, то:
            {
                ++rAns;
                money += 5000; // Прибавить к числу money (5000).
                getMoney = true; // (Если деньги уже даны, то равно true).
                PlayerPrefs.SetInt("MR", rAns); // Сохранить кол-во денег для отображения при окончании игры.
                PlayerPrefs.SetInt("M", rAns); // Сохранить кол-во денег для игры.
                PlayerPrefs.Save(); // Сохранить изменения в сохранениях.
            }
            lose = false; // (Если проиграл, то равен true). В этом случае false.
            Stats[0].text = "Правильный ответ"; // Присвоить тексту строку.
            Stats[2].text = "Вопрос: " + wQ.ToString() + "/ 10"; // Присвоить тексту строку.
            Stats[3].text = "Правильных: " + rAns; // Присвоить тексту строку.
        }
        else if (falseA == true) // Иначе если ответ неправильный, то:
        {
            money = 0; // Присвоить деньгам нулевое число.
            lose = true; // (Если проиграл, то равно true)
            Stats[0].text = "Неправильный ответ"; // Присвоить тексту строку.
            Stats[2].text = "Вопрос: " + wQ.ToString() + "/ 10"; // Присвоить тексту строку.
            Stats[3].text = "Правильных: " + rAns; // Присвоить тексту строку.
            PlayerPrefs.SetInt("MR", rAns); // Сохранить кол-во денег для отображения при окончании игры.
            PlayerPrefs.SetInt("M", rAns); // Сохранить кол-во денег для игры.
            PlayerPrefs.Save(); // Сохранить изменения в сохранениях.
        }
        else if (trueA == false && falseA == false) // Иначе если ответ верный и неверный ответы = false, то:
        {
            money = 0; // Присвоить деньгам нулевое число.
            lose = true; // (Если проиграл, то равно true)
            Stats[0].text = "Закончилось время"; // Присвоить тексту строку.
                                                 // Stats[1].enabled = false; // Выключаем текст с фактами о вопросе.
            Stats[2].text = "Вопрос: " + wQ.ToString() + "/ 10"; // Присвоить тексту строку и число.
            Stats[3].text = "Правильных: " + rAns; // Присвоить тексту строку.
            //Stats[3].text = "Ваши деньги: " + money.ToString() + "$"; // Присвоить тексту строку и число.
            //Stats[4].enabled = true; // Включить текст (Нажмите на розовую панель чтобы продолжить)
            PlayerPrefs.SetInt("MR", rAns); // Сохранить кол-во денег для отображения при окончании игры.
            PlayerPrefs.SetInt("M", rAns); // Сохранить кол-во денег для игры.
            PlayerPrefs.Save(); // Сохранить изменения в сохранениях.
        }
        statsAnimation.SetTrigger("Do"); // Выбрать триггер анимации на Do. (Триггер создан в Animator)
    }

    public void ContinueBttn() // Публичная функция (Кнопка продолжения)
    {
        if (true/*lose == false*/) // Если lose = false, то:
        {
            goFadeOut = false; // (Делать переход (из черноты) при загруженом уровне если = true).
            goFadeIn = true; // (Делать переход в черноту при загрузке уровня если = true).
        }
        else // Иначе (Если мы проиграли)
        {
            Application.LoadLevel(0); // Загружаем 0 level (Меню).
        }
    }
    private void continueLoad() // Приватная функция (Продолжить загрузку)
    {
        if (ended == true) // Если прошли полностью игру, то:
        {
            Application.LoadLevel(0); // Загружаем 0 level (Меню).
            PlayerPrefs.SetString("Ended?", "Ended"); // Сохраняем строку Ended.
            PlayerPrefs.Save(); // Сохранить изменения в сохранениях.
        }
        else // Иначе (Если не прошли)
        {
            wQ++; // Прибавить 1 к числу с нумерацией вопроса.
            isLoaded = true; // (Если уровень загружен, то равно true.)
            PlayerPrefs.SetInt("wQ", wQ); // Сохраняем число с нумерацией вопроса.
            PlayerPrefs.Save(); // Сохранить изменения в сохранениях.
            Application.LoadLevel(1); // Загрузить 1 level (Перезапускаем уровень с игрой для нового вопроса).
        }
    }
    public void selectedBttn(int clickBttn) // Публичная функция (Нажатая кнопка)
    {
        clickedAnswer = clickBttn; // Число нажатого ответа = clickBttn.
    }
    public void OnClickMenu() // Публичная функция (При нажатии на кнопку в меню)
    {
        Application.LoadLevel(0); // Загрузить 0 level (Меню)
    }
    public void OnClickExit() // Публичная функция (При нажатии на выход)
    {
        System.Diagnostics.Process.GetCurrentProcess().Kill(); // Полностью закрываем игру.
    }

}