// S001.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

int main()
{
    std::cout << "Hello World!\n";
    char s[256];
    char s1[256];
    char s2[512];
    char f[] = "%s";
    char fd[] = "%d\n";
    _asm {

        lea ebx, s//чтение 1 строки
        push ebx
        lea ecx, f
        push ecx
        call scanf
        add esp, 8

        lea ebx, s1//чтение 2 строки
        push ebx
        lea ecx, f
        push ecx
        call scanf
        add esp, 8

        lea ebx, s// подготовка цикла перезаписи
        mov ecx, -1
        dec ebx
        lea edx, s2
        dec edx
        //цикл перезаписи
        b1 :
        inc ecx
            inc ebx
            inc edx
            mov al, [ebx]
            mov[edx], al
            cmp al, 0
            jnz b1
            
            lea ebx, s1// подготовка цикла перезаписи
            dec edx
            dec ebx
            //цикл перезаписи
            b2 :
        inc edx
            inc ebx
            mov al, [ebx]
            mov[edx], al
            cmp al, 0
            jnz b2
            // Вывод результата
            lea ebx, s2
            push ebx
            lea ecx, f
            push ecx
            call printf
            add esp, 8
    }
    system("pause");
}

