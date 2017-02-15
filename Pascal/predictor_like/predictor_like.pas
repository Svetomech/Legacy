program predictor_like;
uses crt;
var year: word;
begin
   clrscr;
   writeln('Японский гороскоп');
   writeln();
   write('Год: ');
   read(year);
   year:=year mod 12;
   
   case year of
   
     0:
     begin
        clrscr;
        writeln('Японский гороскоп');
        writeln();
        write('Обезьяна');
        writeln();
     end;
     
     1:
     begin
        clrscr;
        writeln('Японский гороскоп');
        writeln();
        write('Петух');
        writeln();
     end;
     
     2:
     begin
        clrscr;
        writeln('Японский гороскоп');
        writeln();
        write('Собака');
        writeln();
     end;
     
     3:
     begin
        clrscr;
        writeln('Японский гороскоп');
        writeln();
        write('Кабан');
        writeln();
     end;
     
     4:
     begin
        clrscr;
        writeln('Японский гороскоп');
        writeln();
        write('Крыса');
        writeln();
     end;
     
     5:
     begin
        clrscr;
        writeln('Японский гороскоп');
        writeln();
        write('Бык');
        writeln();
     end;
     
     6:
     begin
        clrscr;
        writeln('Японский гороскоп');
        writeln();
        write('Тигр');
        writeln();
     end;
     
     7:
     begin
        clrscr;
        writeln('Японский гороскоп');
        writeln();
        write('Заяц');
        writeln();
     end;
     
     8:
     begin
        clrscr;
        writeln('Японский гороскоп');
        writeln();
        write('Дракон');
        writeln();
     end;
     
     9:
     begin
        clrscr;
        writeln('Японский гороскоп');
        writeln();
        write('Змея');
        writeln();
     end;
     
     10:
     begin
        clrscr;
        writeln('Японский гороскоп');
        writeln();
        write('Лошадь');
        writeln();
     end;
     
     11:
     begin
        clrscr;
        writeln('Японский гороскоп');
        writeln();
        write('Овца');
        writeln();
     end;
     
   else
     writeln();
     writeln('IMPOSSIBRU!');
     writeln();
     
   end;

readkey;
end.

