program triangle_equal;
uses crt;
var a,b,S,h: real;
        chs: byte;
const author: string = 'Беляев Андрей';
      companyname: string = 'Svetomech Inc';
begin
   clrscr;
   writeln('Программу написал ученик 9А класса, ', author);
   writeln(companyname, '.');
   writeln();
   writeln('Дано:');
   writeln('________________________________________');
   writeln('Равнобедренный прямоугольный треугольник');
   writeln('1. Катет');
   writeln('2. Гипотенуза');
   writeln('3. Высота');
   writeln('4. Площадь');
   writeln('________________________________________');
   writeln();
   writeln('Найти: любую из трех оставшихся');
   writeln();
   write('Укажите известную (1-4) величину: ');
   read(chs);
   
   case chs of
   
     1:
     begin
        clrscr;
        writeln('Программу написал ученик 9А класса, ', author);
        writeln(companyname, '.');
        writeln();
        writeln('Решение и ответ:');
        writeln('________________________________________');
        write('Катет = '); readln(a);
        {Вычисления}
        b:=a*sqrt(2);
        S:=(a*a)/2;
        h:=a/sqrt(2);
        {Вычисления}
        writeln('Гипотенуза = ', b:1:2); 
        writeln('Высота = ', h:1:2);
        writeln('Площадь = ', S:1:2);
        writeln('________________________________________');
        writeln();
     end;
     
     2:
     begin
        clrscr;
        writeln('Программу написал ученик 9А класса, ', author);
        writeln(companyname, '.');
        writeln();
        writeln('Решение и ответ:');
        writeln('________________________________________');
        write('Гипотенуза = '); readln(b);
        {Вычисления}
        a:=b/sqrt(2);
        S:=(b*b)/4;
        h:=b/2;
        {Вычисления}
        writeln('Катет = ', a:1:2);
        writeln('Высота = ', h:1:2);
        writeln('Площадь = ', S:1:2);
        writeln('________________________________________');
        writeln();
     end;
     
     3:
     begin
        clrscr;
        writeln('Программу написал ученик 9А класса, ', author);
        writeln(companyname, '.');
        writeln();
        writeln('Решение и ответ:');
        writeln('________________________________________');
        write('Высота = '); readln(h);
        {Вычисления}
        S:=(h*h);
        b:=2*h;
        a:=h*sqrt(2);
        {Вычисления}
        writeln('Катет = ', a:1:2);
        writeln('Площадь = ', S:1:2);
        writeln('Гипотенуза = ', b:1:2);
        writeln('________________________________________');
        writeln();
     end;
     
     4:
     begin
        clrscr;
        writeln('Программу написал ученик 9А класса, ', author);
        writeln(companyname, '.');
        writeln();
        writeln('Решение и ответ:');
        writeln('________________________________________');
        write('Площадь = '); readln(S);
        {Вычисления}
        a:=sqrt(2*S);
        b:=2*sqrt(S);
        h:=sqrt(S);
        {Вычисления}
        writeln('Катет = ', a:1:2);
        writeln('Гипотенуза = ', b:1:2);
        writeln('Высота = ', h:1:2);
        writeln('________________________________________');
        writeln();
     end;
     
   else
     writeln();
     writeln('Низзя!');
     writeln();
     
   end;

readkey;
end.

