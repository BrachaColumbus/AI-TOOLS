import os
from PIL import Image

def convert_images_to_grayscale(input_folder, output_folder):
    """
    ממיר את כל התמונות בתיקיית הקלט לגווני אפור (שחור-לבן) ושומר אותן בתיקיית הפלט.

    :param input_folder: הנתיב (Path) לתיקייה המכילה את התמונות המקוריות.
    :param output_folder: הנתיב לתיקייה שבה יישמרו התמונות שהומרו.
    """
    # יצירת תיקיית הפלט אם היא אינה קיימת
    if not os.path.exists(output_folder):
        os.makedirs(output_folder)
        print(f"נוצרה תיקיית פלט: {output_folder}")

    # רשימת סיומות קבצים נפוצות של תמונות שצריך לעבד
    image_extensions = ('.png', '.jpg', '.jpeg', '.bmp', '.tiff', '.gif')

    print(f"מתחיל עיבוד תמונות מתיקייה: {input_folder}")

    # מעבר על כל הקבצים בתיקיית הקלט
    for filename in os.listdir(input_folder):
        # בניית הנתיב המלא לקובץ הקלט
        input_path = os.path.join(input_folder, filename)

        # בדיקה אם הקובץ הוא קובץ תמונה רגיל (לא תיקייה) ושיש לו סיומת רלוונטית
        if os.path.isfile(input_path) and filename.lower().endswith(image_extensions):
            try:
                # פתיחת התמונה
                img = Image.open(input_path)
                
                # המרת התמונה לגווני אפור (שחור-לבן).
                # מצב 'L' ב-Pillow מייצג תמונות גווני אפור.
                grayscale_img = img.convert('L')
                
                # בניית הנתיב המלא לקובץ הפלט
                output_path = os.path.join(output_folder, filename)
                
                # שמירת התמונה שהומרה
                grayscale_img.save(output_path)
                
                print(f"הומר ושמר בהצלחה: {filename} -> {output_path}")

            except Exception as e:
                print(f"שגיאה בעיבוד הקובץ {filename}: {e}")
        
        elif os.path.isdir(input_path):
            # אפשרות להתעלם מתיקיות משנה
            pass
        else:
             print(f"דילוג על קובץ שאינו תמונה או שאינו נתמך: {filename}")
    
    print("--- סיום עיבוד התמונות. ---")

# הגדרות שאת צריכה לשנות:
# 1. הנתיב לתיקייה שבה נמצאות התמונות הצבעוניות שלך
INPUT_DIRECTORY = r"C:\\Users\\The user\\Pictures\\colorful"# 2. הנתיב לתיקייה שבה יישמרו התמונות בשחור-לבן
OUTPUT_DIRECTORY =r"C:\\Users\\The user\\Pictures\\black-white"
# קריאה לפונקציה כדי להתחיל את התהליך
if __name__ == '__main__':
    convert_images_to_grayscale(INPUT_DIRECTORY, OUTPUT_DIRECTORY)