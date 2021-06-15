
#include <iostream>

int main()
{
    char str1[256];
    char str2[256];
    int post;
    int length;
    char f[] = "%s";
    char fd[] = "%d";
    _asm {
        lea ebx, str1
        push ebx
        lea ecx, f
        push ecx
        call scanf
        add esp, 8
        lea ebx, post 
        push ebx
        lea edx, fd
        push edx
        call scanf
        add esp, 8
        lea ebx, length
        push ebx
        lea edx, fd
        push edx
        call scanf
        add esp, 8
        lea ebx, str1
        dec ebx
        mov ecx, -1
        b1:
        inc ecx
            inc ebx
            mov al, [ebx]
            cmp al, 0
            jnz b1
            lea edx, str2
            dec edx//проверка позиции копирования
            mov eax, post
            cmp eax, ecx
            jge end
            cmp eax, 0
            jg skip
            mov eax, 0 
            skip://подготовка к циклу
        lea ebx, str1
            sub ebx, 2
            mov ecx, length
            inc eax//перемещение к нужной позиции
            copy1 :
        inc ebx
            dec eax
            cmp eax, 0
            jnz copy1
            copy2 :
        inc ebx
            inc edx
            dec ecx
            mov al, [ebx]
            mov[edx], al
            cmp al, 0
            jz end
            cmp ecx, 0
            jnz copy2
            jmp end
            end :
        inc edx
            mov[edx], 0
            lea ebx, str2
            push ebx
            lea ecx, f
            push ecx
            call printf
            add esp, 8
    }
}