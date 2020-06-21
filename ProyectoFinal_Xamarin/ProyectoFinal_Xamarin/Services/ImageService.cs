using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProyectoFinal_Xamarin.Converters
{
    public class ImageService
    {


        public ImageSource ConvertImageFromBase64ToImageSource(string imageBase64)
        {
            try
            {
                if (!string.IsNullOrEmpty(imageBase64))
                {
                    try
                    {
                        return ImageSource.FromStream(() =>
                            new MemoryStream(System.Convert.FromBase64String(imageBase64))
                        );
                    }catch(Exception)
                    {
                        return ImageSource.FromStream(() =>
                             new MemoryStream(System.Convert.FromBase64String("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAS4AAACnCAMAAACYVkHVAAAAgVBMVEVYWFrz8/Pz8/X39/dVVVdMTE5JSUv39/n7+/37+/tQUFLm5udubm+7u7ykpKb///+JiI1UU1ixsbNPTlPT09RpaWtDQ0V/f4HKysw+PkDc3N5hYWPu7vDExMZ1dXdlZWeZmZuenqCRkZO1tbd6ens/P0EzMzVIRkyjo6Pg4OPPztPLSG8RAAAHJ0lEQVR4nO2d6XraOhBAjazN8iJw5QVvBENpue//gHdkQzAk0JDSryWa86MYL7ScjkYjIYznIQiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIMgnoX/7H/BkKP5g1N9+R38Sum7Ch9IUXzlgvyVy9lDkfPG339Mf5FsiUNfHQV13Meryg4fgu6HLb1fRA6iN74aumC/o77JY5M7oYp9/j691w4IbRxoj6vogv6fLDgnY0Rfqugll9c+yMpuOj8JQ1034Zhb4PhQhZmyQqOsGlJngtTZdWV+o6wY8luRVV8g813XRmzMLNJoOzYNEOa6Lzutb1/2Ip2NNP+Tewmld3FT5jet+7M+G5kG9cFoXraXc8OvX/Wj8M13dwunGyBJJ5Au7et2lrshtXXzvz0jQXU33uZk2RiLcTvW0G4qq8Gr3SNeTOmImDHdaF4sHXf71dM+aSXhJG4YO61LNISfF19I9LU7hFQydgru6aH8c4ARX0z0rgjHb+2DL7UHQMC86upBX072q20ZKKapijEB3dXmnKoGQ6+me0+28qA/zN+7qUpNP04ioblSrdOLSWV08nJRURMS3RkMnXNVFt6eZLCtMr2/E1wlHdS1YfDbAgSq0Ux9Y2eSqrm+zCwj5yEu5qqsPLn1Buv9QdLn4sewP47+JLnlZ3au3+tzURes3wTVU91NfVJn0Tfp3szGqn+/pmgXdKaB41xCRXcaXm7ry8LItjjSvnWOeSOKL/WV4uaiLep1+1xak+7FaVaoMCCCTi8G3i7o83r5va0aC1vqChuiTAXnRHF3QpRL/XBdtruiazWx1bxvi8fn+vLpwURd9W3RN0v1WGUhbr0/Pe0cXdfGzjywuaUJBJh1BEE3Dy0Fd7xddp/xlmTw96x3d0HWW6lUy/YDnlwQpc3m+i/LwHltwZeS0ru1dwQWEp+boni7eCnKfr0nv6J4udZ7KP+QrcnYp782i6wrktTk6p4tX74+ubyKPzdE1XXR1f3DZqcPIzYXiLP2ELvAVjlMVjumi6spM1y9sEds7urcCxxZdn0N0Di4pGYquz+HiunoViM+iN8w1XbTbpJ9ls6nd0uXZb/koNqJOG8etqygLG+61wQ1xQpeIr68IvwPUdReu6JKbXD2A73bi2gFdwqyTB7Def/noUomd4XrQbTeGEeT8K98yaNT1MFzQ9ThbDuiSQvgPAwr8+Ve+3xmdt/FDabu//Zb+KPRXZfu9fOW2+AdAXQiCIAjyZbHd/PgdRHp8OBx43UuPX1KcHKfq8uhkS01PPNugt2+k869DvWxF68wuWaZZthofhnfEs37uKUpX2YDdV2c19WAHbLKuLygMbrLjUeqNJ9aUqm2/hdpUjRfBntWwEQ1/UbZiTzwmooUul6XWhaJ1IEMOYyCtrTVeSq1Jz3gFj1rPOIwmX3SjPF5pOGz3znr+orWErSa3yynsll7zLoSdTaYY0cxes1mGOuU0gr/IwCmy+diXIf9JFOjatUKUXK2lAF3cBDJV1pYp+kZ3uZGbJE3t/aVUL2WcgxaPhzIuXnzdRWmyh+P2aF4NJ3aeFEmRSt/jjeCgS252lRCRimS7LIM4iYl+edr4ooUsl61siMqrZlhqJE3YcJrJMKesqNbLSta75dIGhOoFkdaf6rVZKj7Xhqul0Zmybx801nnO+UYmOc1TneRWF4XosrqqfNClO8YLGT5teA26SpnouSfjMOQKtmJdMAgK5q0gVfFKlGVpemp16YTsd5VksRyekxmEoTmst4ETNxCFoBfaMo1EtQNdVA26ZonuV6MuakPz5o3A/mVGXboTZa+LJuQ8nC0z3S4TnSoFOajaGWHvOb8eGqNer/WLAV3aTvmxJqATXUYIEYRLkAG6MrnfjY3R6grykERBu/xPb0ddz9o9HnTVpjENJ2HeyaaqfMLmsuR03guzM3oFbcw2Nxtd36tmL1miE2Zv6BUyOtWVQUcKbXWrPAXXLxuRH3QJtpXGH3VRRZonz106e4EsDrp2sTSm3MueE1Hky0RaXRFY8A6NkUUBkXQlZ+Cw1SmbRtewASdVLKeVnud7XeTfWwnpL1B5Gwy6OuXFsn3i3DUUEpGSkFYgunx/yXmkq3wO/WQoiYFUZVfUNGzUpXgsNWWJDPaN3DMK0aWPuUtHw2OpSSV0zG1Hug9lo/heQrzNbGOE1xI6fNrggpxcJjwpVzyNFW3TqBySVNzWLGvDat6nedoOKKu2nENVHrce9JkmrJKhhSblWNaqtF0Nr8h7E5re1mlFGVYbaH0pXE3n7Zon9pXWz2vLvjv4n+fUY8xu0vFnV4YJY8a5ghKBjT/HcjwX/hhWm9hfaBk0MX6YLlXHb+epw6+3UMp4roaXG/aq8UdintnWu5wNHX913ieOIQiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIAiCIMhd/A8jJ6yvI4lkPAAAAABJRU5ErkJggg=="))
                        ); ;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> ConvertImageFileToBase64(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                FileStream stream = File.Open(filePath, FileMode.Open);
                byte[] bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                return System.Convert.ToBase64String(bytes);
            }
            else
            {
                return null;
            }
        }

        public string SaveImageFromBase64(string imageBase64, int id)
        {
            try
            {//LINEA AGREGADA AQUI, FALTA VERIFICAR QUE SUCEDE AQUI=============================================================
                if (!string.IsNullOrEmpty(imageBase64))
                {
                    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), id + ".tmp");
                    byte[] data = Convert.FromBase64String(imageBase64);
                    System.IO.File.WriteAllBytes(filePath, data);
                    return filePath;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e) { return e.ToString(); }
        }

    }
}
