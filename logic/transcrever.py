import sys
import whisper
import os
import whisper.utils

# Entrada: caminho do arquivo e caminho de saída
caminho_audio = sys.argv[1]
caminho_saida = sys.argv[2]

modelo = whisper.load_model("base")

resultado = modelo.transcribe(caminho_audio, language="Portuguese")

# Nome base do arquivo
basename = os.path.splitext(os.path.basename(caminho_audio))[0]

# Salva arquivos
arquivo_txt = os.path.join(caminho_saida, basename + ".txt")
arquivo_srt = os.path.join(caminho_saida, basename + ".srt")
arquivo_vtt = os.path.join(caminho_saida, basename + ".vtt")

with open(arquivo_txt, "w", encoding="utf-8") as f:
    f.write(resultado["text"])

whisper.utils.write_srt(result=resultado, file=arquivo_srt)
whisper.utils.write_vtt(result=resultado, file=arquivo_vtt)

# Retorna os caminhos para o C#
print(arquivo_txt)
print(arquivo_srt)
print(arquivo_vtt)
