using UnityEngine;
using System.Collections.Generic;

namespace Ceramic3d
{
    public class MatrixSolver
    {
        // Основная функция для нахождения всех смещений
        public static List<Matrix4x4> FindAllOffsets(List<Matrix4x4> modelMatrices, 
            List<Matrix4x4> spaceMatrices,
            float epsilon = 0.0001f)
        {
            List<Matrix4x4> offsets = new List<Matrix4x4>();

            foreach (Matrix4x4 m in modelMatrices)
            {
                bool found = false;
                Matrix4x4 invM = m.inverse;

                foreach (Matrix4x4 s in spaceMatrices)
                {
                    // Вычисляем смещение: offset = s * m⁻¹
                    Matrix4x4 offset = s * invM;

                    // Проверяем условие: offset * m ≈ s
                    if (AreMatricesEqual(offset * m, s, epsilon))
                    {
                        offsets.Add(offset);
                        found = true;
                        break; // Нашли подходящее смещение, переходим к следующей матрице модели
                    }
                }

                if (!found)
                {
                    Debug.LogWarning($"Не найдено смещение для матрицы модели: {m}");
                }
            }

            return offsets;
        }

        // Функция сравнения матриц с заданной точностью
        private static bool AreMatricesEqual(Matrix4x4 a, Matrix4x4 b, float epsilon)
        {
            for (int i = 0; i < 16; i++)
            {
                if (Mathf.Abs(a[i] - b[i]) > epsilon)
                    return false;
            }
            return true;
        }
    }
}