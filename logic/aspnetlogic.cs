public async Task<IActionResult> UploadTranscricao(IFormFile file)
{
    var usuarioId = 1; // você deve pegar isso de forma dinâmica

    // Salva o áudio localmente
    var uploadsFolder = Path.Combine("C:\\meus_audios");
    Directory.CreateDirectory(uploadsFolder);
    var caminhoAudio = Path.Combine(uploadsFolder, file.FileName);

    using (var stream = new FileStream(caminhoAudio, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    // Cria pasta de saída temporária
    var pastaSaida = Path.Combine(Path.GetTempPath(), "transcricao_temp");
    Directory.CreateDirectory(pastaSaida);

    // Chama o Python
    var startInfo = new ProcessStartInfo
    {
        FileName = "python",
        Arguments = $"transcrever.py \"{caminhoAudio}\" \"{pastaSaida}\"",
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    string txtPath = "", srtPath = "", vttPath = "";

    using (var process = Process.Start(startInfo))
    {
        var output = await process.StandardOutput.ReadToEndAsync();
        process.WaitForExit();

        var linhas = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        txtPath = linhas[0].Trim();
        srtPath = linhas[1].Trim();
        vttPath = linhas[2].Trim();
    }

    // Lê os arquivos de transcrição
    var texto = await System.IO.File.ReadAllTextAsync(txtPath);
    var srt = await System.IO.File.ReadAllTextAsync(srtPath);
    var vtt = await System.IO.File.ReadAllTextAsync(vttPath);

    // Salva o áudio no banco (opcional, se já não tiver)
    var arquivo = new Arquivo
    {
        NomeOriginal = file.FileName,
        Caminho = caminhoAudio,
        UsuarioId = usuarioId,
        DataEnvio = DateTime.Now,
        ContentType = file.ContentType
    };

    _context.Arquivos.Add(arquivo);
    await _context.SaveChangesAsync();

    // Salva a transcrição
    var transcricao = new Transcricao
    {
        ArquivoId = arquivo.Id,
        UsuarioId = usuarioId,
        Texto = texto,
        ArquivoSrt = srt,
        ArquivoVtt = vtt,
        Data = DateTime.Now
    };

    _context.Transcricoes.Add(transcricao);
    await _context.SaveChangesAsync();

    return Ok("Transcrição salva com sucesso.");
}
