const express = require('express')

module.exports = (server) => {
    const router =  express.Router()
    server.use('/api', router)

    const objetoService = require('../documentos/objetos/objetoService')
    objetoService.register(router, '/objeto')

    router.get('/objeto/getcampos', async (req, res, next) => {
        res.status(200).send(await objetoService.getCampos(req.query.nome));
    })


    const componenteService = require('../documentos/componentes/componenteService')
    componenteService.register(router, '/componente')
    
    router.get('/componente/getbymarcador', async (req, res, next) => {
        res.status(200).send(await componenteService.getByMarcador(req.query.marcador));
    })



}