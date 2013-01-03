// Original author: cvet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline.Den
{
    public class BarbekkyBoy
    {
        // constants for text lines
        class Str
        {
            public const uint StealFail  = 1100;
            public const uint Radio      = 1101;
            public const uint Morning    = 1102;
        }

        public static Critter _BoyInit(IntPtr ptr, bool firstTime)
        {
            Critter boy = new Critter(ptr);
            Init(boy);

            // Создаем событие, где мальчик зазывает покупать у него рации
            if(Global.CreateTimeEvent(Time.After(Time.GameMinute((uint)Global.Random(10, 50))), e_Announcement, boy.Id, false ) == 0)
                Global.Log( "Time event Announcement create fail, {0}", Global.GetLastError() );
            // Создаем событие, где мальчик приветсвует с добрым утром
            if(Global.CreateTimeEvent( Time.GetNearFullSecond( 0, 0, 0, 8, 0, 0 ), e_AnnouncementMorning, boy.Id, false ) == 0)
                Global.Log( "Time event AnnouncementMorning create fail, {0}", Global.GetLastError() );
            return boy;
        }
        public static void Init(Critter boy)
        {
            boy.Stealing += (self, e) =>
            {
                if (e.Success && Global.Random(0, 5) == 0)
                    boy.SayMsg(Say.Norm, TextMsg.Text, Str.StealFail);
            };
            boy.Respawn += (self, e) =>
            {
                if (boy.CountItem(Pid.RADIO) < 1)
                    boy.AddItem(Pid.RADIO, 1);
            };
        }
        // Событие 1
        static uint e_Announcement(IntPtr values_ptr)
        {
            // Ночью не говорим, ждем до утра.
            if(Global.Hour > 20 || Global.Hour < 8 )
                return Time.GetNearFullSecond(0, 0, 0, 8, (ushort)Global.Random( 20, 40 ), 0 ) - Global.FullSecond;

            // Зазываем.
            var values = new UIntArray(values_ptr);
            Critter boy = Global.GetCritter(values[0]);
            if(boy != null)
                boy.SayMsg(Say.NormOnHead, TextMsg.Text, Str.Radio );
            return Time.GameMinute((uint)Global.Random(50, 70));
        }
        // Событие 2
        static uint e_AnnouncementMorning(IntPtr values_ptr)
        {
            var values = new UIntArray(values_ptr);
            Critter boy = Global.GetCritter(values[0]);
            if(boy != null)
                boy.SayMsg(Say.NormOnHead, TextMsg.Text, Str.Morning );
            return 24 * 60 * 60;
        }
        // Создание Хаммера. Ключ вручаем игроку.
        // Вызывается из диалога, в Результате.
        // При ошибке создания Хаммера диалоговая ветка отводится на №9.
        // master - игрок
        // slave - нпц
        // Обойдемся одним игроком.
        public static uint r_CreateHummer(IntPtr player_ptr, IntPtr boy)
        {
            Critter player = (Critter)player_ptr;
            // Берем карту на которой находится игрок с нпц
            Map map = player.Map;
            if(map == null)
            {
                Global.Log("Map nullptr.");
                return 9;
            }

            int pos = Global.Random(0, 4);
            ushort[] x = { 252, 250, 251, 250, 255 };
            ushort[] y = { 182, 186, 192, 195, 192 };

            uint keyId = (uint)Global.Random(10000, 50000); // Генерация номера ключа

            // Хаммер, комплексное создание всесте с багажником
            Item car = map.AddItem(x[pos], y[pos], Pid.HUMMER, 1);
            if(car == null)
                return 9;

            car.LockerId = keyId;

            // Устанавливаем на багажник номер замка
            var bag = car.GetChild(0);
            if(bag != null)
                bag.LockerId = keyId;
            else
                Global.Log( "Bag not created." );

            // Ключ
            var key = player.AddItem(Pid.KEY, 1);
            if(key != null)
                key.LockerId = keyId;
            else
                Global.Log("Create key fail.");

            // Удаляем 10000 монет
            player.DeleteItem(Pid.BOTTLE_CAPS, 10000);
            return 0;
        }
    }
}
