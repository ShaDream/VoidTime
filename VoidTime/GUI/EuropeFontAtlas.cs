using System.Collections.Generic;
using System.Drawing;
using VoidTime.Resources;

namespace VoidTime.GUI
{
    public class EuropeFontAtlas
    {
        #region Hardcode

        private static readonly Dictionary<char, Character> Characters = new Dictionary<char, Character>
        {
            {
                'A',
                new Character('A', new SizeF(48, 54), 0, 2, new Vector2D(0.0004882813f, 0.0625f),
                    new SizeF(0.0234375f, 0.05273438f))
            },
            {
                'B',
                new Character('B', new SizeF(41, 54), 0, 2, new Vector2D(0.04101563f, 0.0625f),
                    new SizeF(0.02001953f, 0.05273438f))
            },
            {
                'C',
                new Character('C', new SizeF(42, 55), 0, 2, new Vector2D(0.08154297f, 0.06347656f),
                    new SizeF(0.02050781f, 0.05371094f))
            },
            {
                'D',
                new Character('D', new SizeF(44, 54), 0, 2, new Vector2D(0.1220703f, 0.0625f),
                    new SizeF(0.02148438f, 0.05273438f))
            },
            {
                'E',
                new Character('E', new SizeF(34, 54), 0, 2, new Vector2D(0.1625977f, 0.0625f),
                    new SizeF(0.01660156f, 0.05273438f))
            },
            {
                'F',
                new Character('F', new SizeF(32, 54), 0, 2, new Vector2D(0.203125f, 0.0625f),
                    new SizeF(0.015625f, 0.05273438f))
            },
            {
                'G',
                new Character('G', new SizeF(44, 55), 0, 2, new Vector2D(0.2436523f, 0.06347656f),
                    new SizeF(0.02148438f, 0.05371094f))
            },
            {
                'H',
                new Character('H', new SizeF(43, 54), 0, 2, new Vector2D(0.2841797f, 0.0625f),
                    new SizeF(0.02099609f, 0.05273438f))
            },
            {
                'I',
                new Character('I', new SizeF(6, 54), 0, 2, new Vector2D(0.324707f, 0.0625f),
                    new SizeF(0.002929688f, 0.05273438f))
            },
            {
                'J',
                new Character('J', new SizeF(33, 55), 0, 2, new Vector2D(0.3652344f, 0.06347656f),
                    new SizeF(0.01611328f, 0.05371094f))
            },
            {
                'K',
                new Character('K', new SizeF(44, 54), 0, 2, new Vector2D(0.4057617f, 0.0625f),
                    new SizeF(0.02148438f, 0.05273438f))
            },
            {
                'L',
                new Character('L', new SizeF(33, 54), 0, 2, new Vector2D(0.4462891f, 0.0625f),
                    new SizeF(0.01611328f, 0.05273438f))
            },
            {
                'M',
                new Character('M', new SizeF(58, 54), 0, 2, new Vector2D(0.4868164f, 0.0625f),
                    new SizeF(0.02832031f, 0.05273438f))
            },
            {
                'N',
                new Character('N', new SizeF(46, 54), 0, 2, new Vector2D(0.5273438f, 0.0625f),
                    new SizeF(0.02246094f, 0.05273438f))
            },
            {
                'O',
                new Character('O', new SizeF(45, 55), 0, 2, new Vector2D(0.5678711f, 0.06347656f),
                    new SizeF(0.02197266f, 0.05371094f))
            },
            {
                'P',
                new Character('P', new SizeF(40, 54), 0, 2, new Vector2D(0.6083984f, 0.0625f),
                    new SizeF(0.01953125f, 0.05273438f))
            },
            {
                'Q',
                new Character('Q', new SizeF(49, 55), 0, 2, new Vector2D(0.6489258f, 0.06347656f),
                    new SizeF(0.02392578f, 0.05371094f))
            },
            {
                'R',
                new Character('R', new SizeF(41, 54), 0, 2, new Vector2D(0.6894531f, 0.0625f),
                    new SizeF(0.02001953f, 0.05273438f))
            },
            {
                'S',
                new Character('S', new SizeF(41, 55), 0, 2, new Vector2D(0.7299805f, 0.06347656f),
                    new SizeF(0.02001953f, 0.05371094f))
            },
            {
                'T',
                new Character('T', new SizeF(42, 54), 0, 2, new Vector2D(0.7705078f, 0.0625f),
                    new SizeF(0.02050781f, 0.05273438f))
            },
            {
                'U',
                new Character('U', new SizeF(42, 55), 0, 2, new Vector2D(0.8110352f, 0.06347656f),
                    new SizeF(0.02050781f, 0.05371094f))
            },
            {
                'V',
                new Character('V', new SizeF(47, 54), 0, 2, new Vector2D(0.8515625f, 0.0625f),
                    new SizeF(0.02294922f, 0.05273438f))
            },
            {
                'W',
                new Character('W', new SizeF(80, 54), 0, 2, new Vector2D(0.8920898f, 0.0625f),
                    new SizeF(0.0390625f, 0.05273438f))
            },
            {
                'X',
                new Character('X', new SizeF(48, 54), 0, 2, new Vector2D(0.9326172f, 0.0625f),
                    new SizeF(0.0234375f, 0.05273438f))
            },
            {
                'Y',
                new Character('Y', new SizeF(46, 54), 0, 2, new Vector2D(0.0004882813f, 0.1435547f),
                    new SizeF(0.02246094f, 0.05273438f))
            },
            {
                'Z',
                new Character('Z', new SizeF(41, 54), 0, 2, new Vector2D(0.04101563f, 0.1435547f),
                    new SizeF(0.02001953f, 0.05273438f))
            },
            {
                'a',
                new Character('a', new SizeF(33, 39), 0, 2, new Vector2D(0.08154297f, 0.1445313f),
                    new SizeF(0.01611328f, 0.03808594f))
            },
            {
                'b',
                new Character('b', new SizeF(33, 55), 0, 2, new Vector2D(0.1220703f, 0.1445313f),
                    new SizeF(0.01611328f, 0.05371094f))
            },
            {
                'c',
                new Character('c', new SizeF(33, 39), 0, 2, new Vector2D(0.1625977f, 0.1445313f),
                    new SizeF(0.01611328f, 0.03808594f))
            },
            {
                'd',
                new Character('d', new SizeF(33, 55), 0, 2, new Vector2D(0.203125f, 0.1445313f),
                    new SizeF(0.01611328f, 0.05371094f))
            },
            {
                'e',
                new Character('e', new SizeF(33, 39), 0, 2, new Vector2D(0.2436523f, 0.1445313f),
                    new SizeF(0.01611328f, 0.03808594f))
            },
            {
                'f',
                new Character('f', new SizeF(21, 54), 0, 2, new Vector2D(0.2841797f, 0.1435547f),
                    new SizeF(0.01025391f, 0.05273438f))
            },
            {
                'g',
                new Character('g', new SizeF(33, 55), 16, 2, new Vector2D(0.324707f, 0.1601563f),
                    new SizeF(0.01611328f, 0.05371094f))
            },
            {
                'h',
                new Character('h', new SizeF(32, 54), 0, 2, new Vector2D(0.3652344f, 0.1435547f),
                    new SizeF(0.015625f, 0.05273438f))
            },
            {
                'i',
                new Character('i', new SizeF(6, 54), 0, 2, new Vector2D(0.4057617f, 0.1435547f),
                    new SizeF(0.002929688f, 0.05273438f))
            },
            {
                'j',
                new Character('j', new SizeF(11, 69), 14, 2, new Vector2D(0.4462891f, 0.1582031f),
                    new SizeF(0.005371094f, 0.06738281f))
            },
            {
                'k',
                new Character('k', new SizeF(31, 54), 0, 2, new Vector2D(0.4868164f, 0.1435547f),
                    new SizeF(0.01513672f, 0.05273438f))
            },
            {
                'l',
                new Character('l', new SizeF(6, 54), 0, 2, new Vector2D(0.5273438f, 0.1435547f),
                    new SizeF(0.002929688f, 0.05273438f))
            },
            {
                'm',
                new Character('m', new SizeF(57, 38), 0, 2, new Vector2D(0.5678711f, 0.1435547f),
                    new SizeF(0.02783203f, 0.03710938f))
            },
            {
                'n',
                new Character('n', new SizeF(32, 38), 0, 2, new Vector2D(0.6083984f, 0.1435547f),
                    new SizeF(0.015625f, 0.03710938f))
            },
            {
                'o',
                new Character('o', new SizeF(33, 39), 0, 2, new Vector2D(0.6489258f, 0.1445313f),
                    new SizeF(0.01611328f, 0.03808594f))
            },
            {
                'p',
                new Character('p', new SizeF(33, 54), 15, 2, new Vector2D(0.6894531f, 0.1591797f),
                    new SizeF(0.01611328f, 0.05273438f))
            },
            {
                'q',
                new Character('q', new SizeF(33, 54), 15, 2, new Vector2D(0.7299805f, 0.1591797f),
                    new SizeF(0.01611328f, 0.05273438f))
            },
            {
                'r',
                new Character('r', new SizeF(27, 38), 0, 2, new Vector2D(0.7705078f, 0.1435547f),
                    new SizeF(0.01318359f, 0.03710938f))
            },
            {
                's',
                new Character('s', new SizeF(32, 39), 0, 2, new Vector2D(0.8110352f, 0.1445313f),
                    new SizeF(0.015625f, 0.03808594f))
            },
            {
                't',
                new Character('t', new SizeF(27, 48), 0, 2, new Vector2D(0.8515625f, 0.1445313f),
                    new SizeF(0.01318359f, 0.046875f))
            },
            {
                'u',
                new Character('u', new SizeF(32, 38), 0, 2, new Vector2D(0.8920898f, 0.1445313f),
                    new SizeF(0.015625f, 0.03710938f))
            },
            {
                'v',
                new Character('v', new SizeF(33, 37), 0, 2, new Vector2D(0.9326172f, 0.1435547f),
                    new SizeF(0.01611328f, 0.03613281f))
            },
            {
                'w',
                new Character('w', new SizeF(54, 37), 0, 2, new Vector2D(0.0004882813f, 0.2246094f),
                    new SizeF(0.02636719f, 0.03613281f))
            },
            {
                'x',
                new Character('x', new SizeF(33, 37), 0, 2, new Vector2D(0.04101563f, 0.2246094f),
                    new SizeF(0.01611328f, 0.03613281f))
            },
            {
                'y',
                new Character('y', new SizeF(32, 54), 16, 2, new Vector2D(0.08154297f, 0.2412109f),
                    new SizeF(0.015625f, 0.05273438f))
            },
            {
                'z',
                new Character('z', new SizeF(29, 37), 0, 2, new Vector2D(0.1220703f, 0.2246094f),
                    new SizeF(0.01416016f, 0.03613281f))
            },
            {
                '0',
                new Character('0', new SizeF(45, 55), 0, 2, new Vector2D(0.1625977f, 0.2255859f),
                    new SizeF(0.02197266f, 0.05371094f))
            },
            {
                '1',
                new Character('1', new SizeF(23, 54), 0, 2, new Vector2D(0.203125f, 0.2246094f),
                    new SizeF(0.01123047f, 0.05273438f))
            },
            {
                '2',
                new Character('2', new SizeF(39, 54), 0, 2, new Vector2D(0.2436523f, 0.2246094f),
                    new SizeF(0.01904297f, 0.05273438f))
            },
            {
                '3',
                new Character('3', new SizeF(40, 55), 0, 2, new Vector2D(0.2841797f, 0.2255859f),
                    new SizeF(0.01953125f, 0.05371094f))
            },
            {
                '4',
                new Character('4', new SizeF(44, 54), 0, 2, new Vector2D(0.324707f, 0.2246094f),
                    new SizeF(0.02148438f, 0.05273438f))
            },
            {
                '5',
                new Character('5', new SizeF(39, 54), 0, 2, new Vector2D(0.3652344f, 0.2255859f),
                    new SizeF(0.01904297f, 0.05273438f))
            },
            {
                '6',
                new Character('6', new SizeF(40, 55), 0, 2, new Vector2D(0.4057617f, 0.2255859f),
                    new SizeF(0.01953125f, 0.05371094f))
            },
            {
                '7',
                new Character('7', new SizeF(40, 54), 0, 2, new Vector2D(0.4462891f, 0.2246094f),
                    new SizeF(0.01953125f, 0.05273438f))
            },
            {
                '8',
                new Character('8', new SizeF(40, 55), 0, 2, new Vector2D(0.4868164f, 0.2255859f),
                    new SizeF(0.01953125f, 0.05371094f))
            },
            {
                '9',
                new Character('9', new SizeF(40, 55), 0, 2, new Vector2D(0.5273438f, 0.2255859f),
                    new SizeF(0.01953125f, 0.05371094f))
            },
            {
                '.',
                new Character('.', new SizeF(7, 7), 0, 2, new Vector2D(0.5678711f, 0.2246094f),
                    new SizeF(0.003417969f, 0.006835938f))
            },
            {
                ',',
                new Character(',', new SizeF(7, 15), 7, 2, new Vector2D(0.6083984f, 0.2324219f),
                    new SizeF(0.003417969f, 0.01464844f))
            },
            {
                ';',
                new Character(';', new SizeF(7, 45), 7, 2, new Vector2D(0.6489258f, 0.2324219f),
                    new SizeF(0.003417969f, 0.04394531f))
            },
            {
                ':',
                new Character(':', new SizeF(7, 37), 0, 2, new Vector2D(0.6894531f, 0.2246094f),
                    new SizeF(0.003417969f, 0.03613281f))
            },
            {
                '?',
                new Character('?', new SizeF(34, 54), 0, 2, new Vector2D(0.7299805f, 0.2246094f),
                    new SizeF(0.01660156f, 0.05273438f))
            },
            {
                '!',
                new Character('!', new SizeF(6, 54), 0, 2, new Vector2D(0.7705078f, 0.2246094f),
                    new SizeF(0.002929688f, 0.05273438f))
            },
            {
                '-',
                new Character('-', new SizeF(36, 3), 0, 2, new Vector2D(0.8110352f, 0.2060547f),
                    new SizeF(0.01757813f, 0.002929688f))
            },
            {
                '_',
                new Character('_', new SizeF(40, 4), 9, 2, new Vector2D(0.8515625f, 0.234375f),
                    new SizeF(0.01953125f, 0.00390625f))
            },
            {
                '~',
                new Character('~', new SizeF(39, 12), 0, 2, new Vector2D(0.8920898f, 0.2041016f),
                    new SizeF(0.01904297f, 0.01171875f))
            },
            {
                '#',
                new Character('#', new SizeF(38, 47), 0, 2, new Vector2D(0.9326172f, 0.2226563f),
                    new SizeF(0.01855469f, 0.04589844f))
            },
            {
                '"',
                new Character('"', new SizeF(18, 22), 0, 2, new Vector2D(0.0004882813f, 0.2744141f),
                    new SizeF(0.008789063f, 0.02148438f))
            },
            {
                '\'',
                new Character('\'', new SizeF(6, 22), 0, 2, new Vector2D(0.04101563f, 0.2744141f),
                    new SizeF(0.002929688f, 0.02148438f))
            },
            {
                '&',
                new Character('&', new SizeF(52, 54), 0, 2, new Vector2D(0.08154297f, 0.3066406f),
                    new SizeF(0.02539063f, 0.05273438f))
            },
            {
                '(',
                new Character('(', new SizeF(14, 63), 8, 2, new Vector2D(0.1220703f, 0.3144531f),
                    new SizeF(0.006835938f, 0.06152344f))
            },
            {
                ')',
                new Character(')', new SizeF(14, 63), 8, 2, new Vector2D(0.1625977f, 0.3144531f),
                    new SizeF(0.006835938f, 0.06152344f))
            },
            {
                '[',
                new Character('[', new SizeF(14, 62), 7, 2, new Vector2D(0.203125f, 0.3134766f),
                    new SizeF(0.006835938f, 0.06054688f))
            },
            {
                ']',
                new Character(']', new SizeF(13, 62), 7, 2, new Vector2D(0.2436523f, 0.3134766f),
                    new SizeF(0.006347656f, 0.06054688f))
            },
            {
                '|',
                new Character('|', new SizeF(6, 68), 13, 2, new Vector2D(0.2841797f, 0.3193359f),
                    new SizeF(0.002929688f, 0.06640625f))
            },
            {
                '`',
                new Character('`', new SizeF(13, 11), 0, 2, new Vector2D(0.324707f, 0.2646484f),
                    new SizeF(0.006347656f, 0.01074219f))
            },
            {
                '\\',
                new Character('\\', new SizeF(46, 62), 7, 2, new Vector2D(0.3652344f, 0.3134766f),
                    new SizeF(0.02246094f, 0.06054688f))
            },
            {
                '/',
                new Character('/', new SizeF(46, 62), 7, 2, new Vector2D(0.4057617f, 0.3134766f),
                    new SizeF(0.02246094f, 0.06054688f))
            },
            {
                '@',
                new Character('@', new SizeF(44, 46), 0, 2, new Vector2D(0.4462891f, 0.3046875f),
                    new SizeF(0.02148438f, 0.04492188f))
            },
            {
                '°',
                new Character('°', new SizeF(23, 22), 0, 2, new Vector2D(0.4868164f, 0.2753906f),
                    new SizeF(0.01123047f, 0.02148438f))
            },
            {
                '+',
                new Character('+', new SizeF(37, 34), 0, 2, new Vector2D(0.5273438f, 0.2958984f),
                    new SizeF(0.01806641f, 0.03320313f))
            },
            {
                '=',
                new Character('=', new SizeF(37, 15), 0, 2, new Vector2D(0.5678711f, 0.2861328f),
                    new SizeF(0.01806641f, 0.01464844f))
            },
            {
                '*',
                new Character('*', new SizeF(29, 28), 0, 2, new Vector2D(0.6083984f, 0.2802734f),
                    new SizeF(0.01416016f, 0.02734375f))
            },
            {
                '$',
                new Character('$', new SizeF(45, 65), 5, 2, new Vector2D(0.6489258f, 0.3115234f),
                    new SizeF(0.02197266f, 0.06347656f))
            },
            {
                '£',
                new Character('£', new SizeF(19, 15), 7, 2, new Vector2D(0.6894531f, 0.3134766f),
                    new SizeF(0.009277344f, 0.01464844f))
            },
            {
                '€',
                new Character('€', new SizeF(32, 52), 0, 2, new Vector2D(0.7299805f, 0.3066406f),
                    new SizeF(0.015625f, 0.05078125f))
            },
            {
                '<',
                new Character('<', new SizeF(35, 38), 0, 2, new Vector2D(0.7705078f, 0.2978516f),
                    new SizeF(0.01708984f, 0.03710938f))
            },
            {
                '>',
                new Character('>', new SizeF(35, 38), 0, 2, new Vector2D(0.8110352f, 0.2978516f),
                    new SizeF(0.01708984f, 0.03710938f))
            },
            {
                '%',
                new Character('%', new SizeF(60, 54), 0, 2, new Vector2D(0.8515625f, 0.3056641f),
                    new SizeF(0.02929688f, 0.05273438f))
            },
            {
                'А',
                new Character('А', new SizeF(48, 54), 0, 2, new Vector2D(0.8920898f, 0.3056641f),
                    new SizeF(0.0234375f, 0.05273438f))
            },
            {
                'Б',
                new Character('Б', new SizeF(40, 54), 0, 2, new Vector2D(0.9326172f, 0.3056641f),
                    new SizeF(0.01953125f, 0.05273438f))
            },
            {
                'В',
                new Character('В', new SizeF(41, 54), 0, 2, new Vector2D(0.0004882813f, 0.3867188f),
                    new SizeF(0.02001953f, 0.05273438f))
            },
            {
                'Г',
                new Character('Г', new SizeF(33, 54), 0, 2, new Vector2D(0.04101563f, 0.3867188f),
                    new SizeF(0.01611328f, 0.05273438f))
            },
            {
                'Д',
                new Character('Д', new SizeF(49, 67), 12, 2, new Vector2D(0.08154297f, 0.3994141f),
                    new SizeF(0.02392578f, 0.06542969f))
            },
            {
                'Е',
                new Character('Е', new SizeF(34, 54), 0, 2, new Vector2D(0.1220703f, 0.3867188f),
                    new SizeF(0.01660156f, 0.05273438f))
            },
            {
                'Ё',
                new Character('Ё', new SizeF(27, 62), 0, 2, new Vector2D(0.1625977f, 0.3867188f),
                    new SizeF(0.01318359f, 0.06054688f))
            },
            {
                'Ж',
                new Character('Ж', new SizeF(75, 54), 0, 2, new Vector2D(0.203125f, 0.3867188f),
                    new SizeF(0.03662109f, 0.05273438f))
            },
            {
                'З',
                new Character('З', new SizeF(42, 55), 0, 2, new Vector2D(0.2436523f, 0.3876953f),
                    new SizeF(0.02050781f, 0.05371094f))
            },
            {
                'И',
                new Character('И', new SizeF(46, 54), 0, 2, new Vector2D(0.2841797f, 0.3867188f),
                    new SizeF(0.02246094f, 0.05273438f))
            },
            {
                'Й',
                new Character('Й', new SizeF(46, 63), 0, 2, new Vector2D(0.324707f, 0.3867188f),
                    new SizeF(0.02246094f, 0.06152344f))
            },
            {
                'К',
                new Character('К', new SizeF(44, 54), 0, 2, new Vector2D(0.3652344f, 0.3867188f),
                    new SizeF(0.02148438f, 0.05273438f))
            },
            {
                'Л',
                new Character('Л', new SizeF(46, 54), 0, 2, new Vector2D(0.4057617f, 0.3867188f),
                    new SizeF(0.02246094f, 0.05273438f))
            },
            {
                'М',
                new Character('М', new SizeF(58, 54), 0, 2, new Vector2D(0.4462891f, 0.3867188f),
                    new SizeF(0.02832031f, 0.05273438f))
            },
            {
                'Н',
                new Character('Н', new SizeF(43, 54), 0, 2, new Vector2D(0.4868164f, 0.3867188f),
                    new SizeF(0.02099609f, 0.05273438f))
            },
            {
                'О',
                new Character('О', new SizeF(45, 55), 0, 2, new Vector2D(0.5273438f, 0.3876953f),
                    new SizeF(0.02197266f, 0.05371094f))
            },
            {
                'П',
                new Character('П', new SizeF(43, 54), 0, 2, new Vector2D(0.5678711f, 0.3867188f),
                    new SizeF(0.02099609f, 0.05273438f))
            },
            {
                'Р',
                new Character('Р', new SizeF(40, 54), 0, 2, new Vector2D(0.6083984f, 0.3867188f),
                    new SizeF(0.01953125f, 0.05273438f))
            },
            {
                'С',
                new Character('С', new SizeF(42, 55), 0, 2, new Vector2D(0.6489258f, 0.3876953f),
                    new SizeF(0.02050781f, 0.05371094f))
            },
            {
                'Т',
                new Character('Т', new SizeF(42, 54), 0, 2, new Vector2D(0.6894531f, 0.3867188f),
                    new SizeF(0.02050781f, 0.05273438f))
            },
            {
                'У',
                new Character('У', new SizeF(44, 54), 0, 2, new Vector2D(0.7299805f, 0.3867188f),
                    new SizeF(0.02148438f, 0.05273438f))
            },
            {
                'Ф',
                new Character('Ф', new SizeF(56, 54), 0, 2, new Vector2D(0.7705078f, 0.3867188f),
                    new SizeF(0.02734375f, 0.05273438f))
            },
            {
                'Х',
                new Character('Х', new SizeF(48, 54), 0, 2, new Vector2D(0.8110352f, 0.3867188f),
                    new SizeF(0.0234375f, 0.05273438f))
            },
            {
                'Ц',
                new Character('Ц', new SizeF(47, 67), 12, 2, new Vector2D(0.8515625f, 0.3994141f),
                    new SizeF(0.02294922f, 0.06542969f))
            },
            {
                'Ч',
                new Character('Ч', new SizeF(40, 54), 0, 2, new Vector2D(0.8920898f, 0.3867188f),
                    new SizeF(0.01953125f, 0.05273438f))
            },
            {
                'Ш',
                new Character('Ш', new SizeF(73, 54), 0, 2, new Vector2D(0.9326172f, 0.3867188f),
                    new SizeF(0.03564453f, 0.05273438f))
            },
            {
                'Щ',
                new Character('Щ', new SizeF(76, 67), 12, 2, new Vector2D(0.0004882813f, 0.4804688f),
                    new SizeF(0.03710938f, 0.06542969f))
            },
            {
                'Ъ',
                new Character('Ъ', new SizeF(49, 54), 0, 2, new Vector2D(0.04101563f, 0.4677734f),
                    new SizeF(0.02392578f, 0.05273438f))
            },
            {
                'Ы',
                new Character('Ы', new SizeF(55, 54), 0, 2, new Vector2D(0.08154297f, 0.4677734f),
                    new SizeF(0.02685547f, 0.05273438f))
            },
            {
                'Ь',
                new Character('Ь', new SizeF(40, 54), 0, 2, new Vector2D(0.1220703f, 0.4677734f),
                    new SizeF(0.01953125f, 0.05273438f))
            },
            {
                'Э',
                new Character('Э', new SizeF(42, 55), 0, 2, new Vector2D(0.1625977f, 0.46875f),
                    new SizeF(0.02050781f, 0.05371094f))
            },
            {
                'Ю',
                new Character('Ю', new SizeF(60, 55), 0, 2, new Vector2D(0.203125f, 0.46875f),
                    new SizeF(0.02929688f, 0.05371094f))
            },
            {
                'Я',
                new Character('Я', new SizeF(41, 54), 0, 2, new Vector2D(0.2436523f, 0.4677734f),
                    new SizeF(0.02001953f, 0.05273438f))
            },
            {
                'а',
                new Character('а', new SizeF(33, 39), 0, 2, new Vector2D(0.2841797f, 0.46875f),
                    new SizeF(0.01611328f, 0.03808594f))
            },
            {
                'б',
                new Character('б', new SizeF(33, 53), 0, 2, new Vector2D(0.324707f, 0.46875f),
                    new SizeF(0.01611328f, 0.05175781f))
            },
            {
                'в',
                new Character('в', new SizeF(32, 37), 0, 2, new Vector2D(0.3652344f, 0.4677734f),
                    new SizeF(0.015625f, 0.03613281f))
            },
            {
                'г',
                new Character('г', new SizeF(26, 37), 0, 2, new Vector2D(0.4057617f, 0.4677734f),
                    new SizeF(0.01269531f, 0.03613281f))
            },
            {
                'д',
                new Character('д', new SizeF(38, 48), 10, 2, new Vector2D(0.4462891f, 0.4785156f),
                    new SizeF(0.01855469f, 0.046875f))
            },
            {
                'е',
                new Character('е', new SizeF(33, 39), 0, 2, new Vector2D(0.4868164f, 0.46875f),
                    new SizeF(0.01611328f, 0.03808594f))
            },
            {
                'ё',
                new Character('ё', new SizeF(31, 50), 0, 2, new Vector2D(0.5273438f, 0.46875f),
                    new SizeF(0.01513672f, 0.04882813f))
            },
            {
                'ж',
                new Character('ж', new SizeF(51, 37), 0, 2, new Vector2D(0.5678711f, 0.4677734f),
                    new SizeF(0.02490234f, 0.03613281f))
            },
            {
                'з',
                new Character('з', new SizeF(32, 39), 0, 2, new Vector2D(0.6083984f, 0.46875f),
                    new SizeF(0.015625f, 0.03808594f))
            },
            {
                'и',
                new Character('и', new SizeF(32, 37), 0, 2, new Vector2D(0.6489258f, 0.4677734f),
                    new SizeF(0.015625f, 0.03613281f))
            },
            {
                'й',
                new Character('й', new SizeF(32, 48), 0, 2, new Vector2D(0.6894531f, 0.4677734f),
                    new SizeF(0.015625f, 0.046875f))
            },
            {
                'к',
                new Character('к', new SizeF(31, 37), 0, 2, new Vector2D(0.7299805f, 0.4677734f),
                    new SizeF(0.01513672f, 0.03613281f))
            },
            {
                'л',
                new Character('л', new SizeF(33, 37), 0, 2, new Vector2D(0.7705078f, 0.4677734f),
                    new SizeF(0.01611328f, 0.03613281f))
            },
            {
                'м',
                new Character('м', new SizeF(43, 37), 0, 2, new Vector2D(0.8110352f, 0.4677734f),
                    new SizeF(0.02099609f, 0.03613281f))
            },
            {
                'н',
                new Character('н', new SizeF(32, 37), 0, 2, new Vector2D(0.8515625f, 0.4677734f),
                    new SizeF(0.015625f, 0.03613281f))
            },
            {
                'о',
                new Character('о', new SizeF(33, 39), 0, 2, new Vector2D(0.8920898f, 0.46875f),
                    new SizeF(0.01611328f, 0.03808594f))
            },
            {
                'п',
                new Character('п', new SizeF(31, 37), 0, 2, new Vector2D(0.9326172f, 0.4677734f),
                    new SizeF(0.01513672f, 0.03613281f))
            },
            {
                'р',
                new Character('р', new SizeF(33, 54), 15, 2, new Vector2D(0.0004882813f, 0.5644531f),
                    new SizeF(0.01611328f, 0.05273438f))
            },
            {
                'с',
                new Character('с', new SizeF(33, 39), 0, 2, new Vector2D(0.04101563f, 0.5498047f),
                    new SizeF(0.01611328f, 0.03808594f))
            },
            {
                'т',
                new Character('т', new SizeF(32, 37), 0, 2, new Vector2D(0.08154297f, 0.5488281f),
                    new SizeF(0.015625f, 0.03613281f))
            },
            {
                'у',
                new Character('у', new SizeF(32, 54), 16, 2, new Vector2D(0.1220703f, 0.5654297f),
                    new SizeF(0.015625f, 0.05273438f))
            },
            {
                'ф',
                new Character('ф', new SizeF(52, 70), 15, 2, new Vector2D(0.1625977f, 0.5644531f),
                    new SizeF(0.02539063f, 0.06835938f))
            },
            {
                'х',
                new Character('х', new SizeF(33, 37), 0, 2, new Vector2D(0.203125f, 0.5488281f),
                    new SizeF(0.01611328f, 0.03613281f))
            },
            {
                'ц',
                new Character('ц', new SizeF(35, 48), 10, 2, new Vector2D(0.2436523f, 0.5595703f),
                    new SizeF(0.01708984f, 0.046875f))
            },
            {
                'ч',
                new Character('ч', new SizeF(30, 37), 0, 2, new Vector2D(0.2841797f, 0.5488281f),
                    new SizeF(0.01464844f, 0.03613281f))
            },
            {
                'ш',
                new Character('ш', new SizeF(53, 37), 0, 2, new Vector2D(0.324707f, 0.5488281f),
                    new SizeF(0.02587891f, 0.03613281f))
            },
            {
                'щ',
                new Character('щ', new SizeF(57, 48), 10, 2, new Vector2D(0.3652344f, 0.5595703f),
                    new SizeF(0.02783203f, 0.046875f))
            },
            {
                'ъ',
                new Character('ъ', new SizeF(38, 37), 0, 2, new Vector2D(0.4057617f, 0.5488281f),
                    new SizeF(0.01855469f, 0.03613281f))
            },
            {
                'ы',
                new Character('ы', new SizeF(44, 37), 0, 2, new Vector2D(0.4462891f, 0.5488281f),
                    new SizeF(0.02148438f, 0.03613281f))
            },
            {
                'ь',
                new Character('ь', new SizeF(31, 37), 0, 2, new Vector2D(0.4868164f, 0.5488281f),
                    new SizeF(0.01513672f, 0.03613281f))
            },
            {
                'э',
                new Character('э', new SizeF(32, 39), 0, 2, new Vector2D(0.5273438f, 0.5498047f),
                    new SizeF(0.015625f, 0.03808594f))
            },
            {
                'ю',
                new Character('ю', new SizeF(48, 39), 0, 2, new Vector2D(0.5678711f, 0.5498047f),
                    new SizeF(0.0234375f, 0.03808594f))
            },
            {
                'я',
                new Character('я', new SizeF(31, 37), 0, 2, new Vector2D(0.6083984f, 0.5488281f),
                    new SizeF(0.01513672f, 0.03613281f))
            }
        };

        #endregion

        public static FontAtlas GetAtlas()
        {
            return new FontAtlas(Characters, Textures.EuropeFont);
        }
    }
}