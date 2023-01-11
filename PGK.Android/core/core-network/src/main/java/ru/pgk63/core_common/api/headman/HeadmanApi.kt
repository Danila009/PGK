package ru.pgk63.core_common.api.headman

import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path
import ru.pgk63.core_common.api.headman.model.HeadmanUpdateRaportichkaRowBody

interface HeadmanApi {

    @PUT("/pgk63/api/Headman/Raportichka/Row/{id}")
    suspend fun updateRaportichkaRow(
        @Path("id") rowId:Int,
        @Body body: HeadmanUpdateRaportichkaRowBody
    ): Response<Unit?>

    @POST("/pgk63/api/Headman/Raportichka")
    suspend fun createRaportichka(): Response<Unit?>
}