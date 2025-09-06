namespace TimeLibrary
{
    public class Time
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
        public int Milliseconds { get; private set; }

        // 1) Constructor sin parámetros
        public Time() : this(0, 0, 0, 0) { }

        // 2) Constructor con horas
        public Time(int hours) : this(hours, 0, 0, 0) { }

        // 3) Constructor con horas y minutos
        public Time(int hours, int minutes) : this(hours, minutes, 0, 0) { }

        // 4) Constructor con horas, minutos y segundos
        public Time(int hours, int minutes, int seconds) : this(hours, minutes, seconds, 0) { }

        // 5) Constructor con horas, minutos, segundos y milisegundos
        public Time(int hours, int minutes, int seconds, int milliseconds)
        {
            if (hours < 0 || hours > 23) throw new ArgumentException($"The hour: {hours}, is not valid.");
            if (minutes < 0 || minutes > 59) throw new ArgumentException("Minutos inválidos");
            if (seconds < 0 || seconds > 59) throw new ArgumentException("Segundos inválidos");
            if (milliseconds < 0 || milliseconds > 999) throw new ArgumentException("Milisegundos inválidos");

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        public override string ToString()
        {
            DateTime dt = new DateTime(1, 1, 1, Hours, Minutes, Seconds, Milliseconds);
            return dt.ToString("hh:mm:ss.fff tt"); // formato no militar
        }

        public long ToMilliseconds() =>
            ((Hours * 3600L + Minutes * 60L + Seconds) * 1000L) + Milliseconds;

        public long ToSeconds() =>
            Hours * 3600 + Minutes * 60 + Seconds;

        public long ToMinutes() =>
            Hours * 60 + Minutes;

        public bool IsOtherDay(Time other)
        {
            long total = this.ToMilliseconds() + other.ToMilliseconds();
            return total >= 24 * 3600 * 1000;
        }

        public Time Add(Time other)
        {
            int totalMs = this.Milliseconds + other.Milliseconds;
            int extraSec = totalMs / 1000;
            int ms = totalMs % 1000;

            int totalSec = this.Seconds + other.Seconds + extraSec;
            int extraMin = totalSec / 60;
            int sec = totalSec % 60;

            int totalMin = this.Minutes + other.Minutes + extraMin;
            int extraHr = totalMin / 60;
            int min = totalMin % 60;

            int totalHr = this.Hours + other.Hours + extraHr;
            int hr = totalHr % 24; // si pasa de 23 reinicia al otro día

            return new Time(hr, min, sec, ms);
        }
    }
}
